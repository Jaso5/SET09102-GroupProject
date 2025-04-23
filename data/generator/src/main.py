from pyodbc import connect, Cursor, ProgrammingError
from datetime import datetime
import json

def ins(cur: Cursor, sql: str) -> int:
    try:
        cur.execute(sql)
    except ProgrammingError as err:
        print(f"SQL Error: {err}\n{sql}")
        raise err
        
    return cur.fetchone()[0] # type: ignore

class Quantity:
    quantity: str
    symbol: str
    unit: str
    desc: str
    freq: float
    safe_level: float
    id: int

    def __init__(
        self,
        quantity: str,
        symbol: str,
        unit: str,
        description: str,
        frequency: str,
        safe_level: str,
    ) -> None:
        self.quantity = quantity
        self.symbol = symbol
        self.unit = unit
        self.desc = description

        if frequency == "Hourly":
            self.freq = 60 * 60
        elif frequency == "Daily":
            self.freq = 60 * 60 * 24
        else:
            raise ValueError(f"Unknown frequency: {frequency}")
        
        if safe_level != '':
            self.safe_level = float(safe_level)
        else:
            self.safe_level = -1

    def serialize(self) -> str:
        return \
f"INSERT INTO dbo.Quantities(quantity, [symbol], unit, [description], safe_level, min_threshold, max_threshold) output inserted.quantity_Id \
VALUES('{self.quantity}', '{self.symbol}', '{self.unit}', '{self.desc}', {self.safe_level}, 0.0, 1.0)"

    def insert(self, cur: Cursor):
        self.id = ins(cur, self.serialize())

class VirtualSensor:
    category: str

    quantity: Quantity
    ref: str
    sensor: str
    URL: str
    id: int

    readings: list[tuple[datetime, float | None]]

    def __init__(
        self,
        category: str,
        quantity: str,
        symbol: str,
        unit: str,
        description: str,
        frequency: str,
        safe_level: str,
        ref: str,
        sensor: str,
        url: str
    ) -> None:
        self.category = category
        self.quantity = Quantity(quantity, symbol, unit, description, frequency, safe_level)
        self.ref = ref
        self.sensor = sensor
        self.url = url
        self.readings = []

    def serialize(self, parent_id: int) -> str:
        return \
f"INSERT INTO dbo.VirtualSensors(reference, catergory, sensor_type, url, r_sensor_Id, quantity_id) output inserted.v_sensor_Id \
VALUES('{self.ref}', '{self.category}', '{self.sensor}', '{self.url}', {parent_id}, {self.quantity.id});"
    
    def serialize_readings(self) -> str:
        # filter_no_values = lambda r: r[1] != None
        tuplize = lambda r: f"('{r[0]}', {r[1]}, {self.id})"
        return \
f"""INSERT INTO Readings([timestamp], [value], v_sensor_id) output inserted.reading_Id VALUES
{",\n".join(map(tuplize, self.readings))};"""
    
    def insert(self, cur: Cursor, parent_id: int):
        print(f"Inserting sensor {self.quantity.symbol}")
        self.quantity.insert(cur)
        self.id = ins(cur, self.serialize(parent_id))

        ins(cur, self.serialize_readings())


class RealSensor:
    lat: float
    lon: float
    freq: float

    id: int
    sensors: list[VirtualSensor]

    def __init__(self, lat: float, lon: float, freq: int, sensors: list[VirtualSensor]):
        self.lat = lat
        self.lon = lon
        self.freq = freq
        self.sensors = sensors

    def serialize(self) -> str:
        return \
f"INSERT INTO dbo.RealSensors(lat, lon, frequency, [status]) output inserted.r_sensor_Id \
VALUES({self.lat}, {self.lon}, {self.freq}, 1.0);"
    
    def insert(self, cur: Cursor):
        self.id = ins(cur, self.serialize())
        list(map(lambda s: s.insert(cur, self.id), self.sensors))

def parse_metadata() -> list[VirtualSensor]:
    l = []

    file = open("csv/metadata.csv")

    # Skip first line as it's a header
    file.readline()

    for line in file:
        (category, quantity, symbol, unit, description, frequency,
         safe_level, ref, sensor, url) = line.rstrip('\n').split(',')
        
        # print(f"Safe level: {safe_level}")

        l.append(VirtualSensor(category, quantity, symbol, unit, description,
                 frequency, safe_level, ref, sensor, url))

    return l

def parse_readings(sensors: list[VirtualSensor], path: str, date_format: str, hour_override=False) -> RealSensor:
    file = open(path)
    
    SEP = ","

    (lat, lon, *_) = file.readline().rstrip("\n").rstrip(SEP).split(SEP)
    (_, *values) =   file.readline().rstrip("\n").rstrip(SEP).split(SEP) # Header


    target_sensors: list[VirtualSensor] = []

    for value in values:
        for sensor in sensors:
            if (value == sensor.quantity.symbol):
                target_sensors.append(sensor)
                break

    for line in file:
        (date_str, *values) = line.rstrip("\n").rstrip(SEP).split(SEP)
        if hour_override:
            # Some files incorrectly store the hour in a range of 01..24
            date_str = date_str[:11] + str(int(date_str[11:13]) - 1).zfill(2) + date_str[13:]
        date = datetime.strptime(date_str, date_format)
        for (sensor, value) in zip(target_sensors, values):
            if value == "No data":
                continue
                # sensor.readings.append((date, None))
            else:
                sensor.readings.append((date, float(value)))

    return RealSensor(float(lat), float(lon), int(target_sensors[0].quantity.freq), target_sensors)
    
def main():
    try:
        # Load the settings from the main application
        settings = json.load(open("../../environmentMonitoring/environmentMonitoring.Database/appsettings.json"))    
    except Exception as err:
        print("Failed to open appsettings.json, please populate the file using the example file")
        print(err)

    pre_conn: str = settings["ConnectionStrings"]["DevelopmentConnection"]
    # Microsoft struggle to keep anything consistent, so we need to replace some keys
    pre_conn = pre_conn \
        .replace("Server=", "SERVER=") \
        .replace("Database=", "DATABASE=") \
        .replace("User Id=", "UID=") \
        .replace("Password=", "PWD=") \
        .rstrip(";TrustServerCertificate=True;Encrypt=True;")

    conn_string = f"DRIVER={{ODBC Driver 18 for SQL Server}};{pre_conn};TrustServerCertificate=yes;"

    try:
        conn = connect(conn_string)
    except Exception as err:
        print(f"Could not connect to database: {err}")

    cur = conn.cursor()

    v_sensors = parse_metadata()
    r_sensors: list[RealSensor] = []

    r_sensors.append(parse_readings(v_sensors, "csv/Weather.csv", "%Y-%m-%dT%H:%M"))
    r_sensors.append(parse_readings(v_sensors, "csv/Water_quality.csv", "%d/%m/%Y %H:%M:%S", hour_override=True))
    r_sensors.append(parse_readings(v_sensors, "csv/Air_quality.csv", "%d/%m/%Y %H:%M:%S", hour_override=True))

    for sensor in r_sensors:
        print("Inserting Real Sensor")
        sensor.insert(cur)
    
    cur.commit()

if __name__ == "__main__":
    main()
