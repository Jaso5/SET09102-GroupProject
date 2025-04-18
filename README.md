# Environmental Monitoring Application

## Setup

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