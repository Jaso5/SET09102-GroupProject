from mariadb import connect, Cursor, OperationalError

from datetime import datetime

class Quantity:
    quantity: str
    symbol: str
    unit: str
    desc: str
    freq: float
    safe_level: float

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


class VirtualSensor:
    category: str

    quantity: Quantity
    ref: str
    sensor: str
    URL: str

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

    def serialize(self) -> str:
        raise NotImplemented

class RealSensor:
    lat: float
    lon: float

    freq: float

    sensors: list[VirtualSensor]

def run_query(cur: Cursor, query: str):
    if query == "":  # It's possible to get empty statements, this will throw an error
        return
    try:
        cur.execute(query)
    except Exception as err:
        print(f"Query: {query}")
        print(f"Error: {err}")


def init_db(cur: Cursor):
    query = open("sql/init.sql").read()

    # Split our query up into multiple sub-queries
    subq = query.split(";")

    for query in subq:
        run_query(cur, query)


def parse_metadata() -> list[VirtualSensor]:
    l = []

    file = open("csv/metadata.csv")

    # Skip first line as it's a header
    file.readline()

    for line in file:
        (category, quantity, symbol, unit, description, frequency,
         safe_level, ref, sensor, url) = line.split(',')

        l.append(VirtualSensor(category, quantity, symbol, unit, description,
                 frequency, safe_level, ref, sensor, url))

    return l

def parse_readings(sensors: list[VirtualSensor], path: str, date_format: str, hour_override=False):
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
                sensor.readings.append((date, None))
            else:
                sensor.readings.append((date, float(value)))


def main():
    # try:
    #     con = connect(
    #         host="127.0.0.1",
    #         port=3306,
    #         user="root",
    #         password=""
    #     )
    # except OperationalError as err:
    #     print(f"ERROR: {err}")
    #     exit(-1)

    # cur: Cursor = con.cursor()

    # init_db(cur)

    sensors = parse_metadata()

    parse_readings(sensors, "csv/Weather.csv", "%Y-%m-%dT%H:%M")
    parse_readings(sensors, "csv/Water_quality.csv", "%d/%m/%Y %H:%M:%S", hour_override=True)
    parse_readings(sensors, "csv/Air_quality.csv", "%d/%m/%Y %H:%M:%S", hour_override=True)


    print(f"{sensors}")

if __name__ == "__main__":
    main()
