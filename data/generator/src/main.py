from mariadb import connect, Cursor, OperationalError

# Weather.csv: "%Y-%m-%dT%H:%M"


class Sensor:
    category: str
    quantity: str
    symbol: str
    unit: str
    desc: str
    freq: int
    safe_level: float
    ref: str
    sensor: str
    URL: str

    readings: list[tuple[str, str, float]]

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

        self.ref = ref
        self.sensor = sensor
        self.url = url

    def serialize(self) -> str:
        raise NotImplemented


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


def parse_metadata() -> list[Sensor]:
    l = []

    file = open("csv/metadata.csv")

    # Skip first line as it's a header
    file.readline()

    for line in file:
        (category, quantity, symbol, unit, description, frequency,
         safe_level, ref, sensor, url) = line.split(',')

        l.append(Sensor(category, quantity, symbol, unit, description,
                 frequency, safe_level, ref, sensor, url))

    return l


def main():
    try:
        con = connect(
            host="127.0.0.1",
            port=3306,
            user="root",
            password=""
        )
    except OperationalError as err:
        print(f"ERROR: {err}")
        exit(-1)

    cur: Cursor = con.cursor()

    init_db(cur)

    sensors = parse_metadata()

    print(f"{sensors}")

if __name__ == "__main__":
    main()
