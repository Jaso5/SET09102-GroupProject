# Environmental Monitoring Application

## Setup

### Creating Database

Using docker create the container for the database:

```sh
docker pull mcr.microsoft.com/mssql/server:2022-latest
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourPassw0rd>" -p 1433:1433 --name sql1 --hostname sql1 -d mcr.microsoft.com/mssql/server:2022-latest
```

Create the initial database using this query in Azure Data Studio (ADS):

```sql
CREATE DATABASE envMon;
```

Create an app login for the database, example:

```sql
CREATE LOGIN envMonApp WITH PASSWORD='envM0nApp$';
CREATE user envMonApp for login envMonApp;
GRANT control on DATABASE::envMon to envMonApp;
```

Populate `environmentMonitoring\environmentMonitoring.Database\appsettings.json`, there is an example you can use in the same directory. Use the username and password from the previous step.

Next run the migrations to create the database

```sh
cd environmentMonitoring
dotnet ef --project environmentMonitoring.Database --startup-project environmentMonitoring.Migrations database update
```

This change should be reflected in ADS

### Populating Database

This requires `python >= 3.13`, [Poetry](https://python-poetry.org/docs/#installation), and [MSSQL ODBC driver](https://learn.microsoft.com/en-us/sql/connect/python/pyodbc/step-1-configure-development-environment-for-pyodbc-python-development?view=sql-server-ver16&tabs=windows#install-the-odbc-driver)

```sh
cd data/generator
poetry install
poetry run generate
```