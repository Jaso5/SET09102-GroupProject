using Microsoft.Data.Sqlite;
using System.IO;

namespace environmentMonitoring.Helpers
{
    public static class DatabaseHelper
    {
        private const string DatabaseFileName = "env.db";
        private const string TableName = "SensorConfigurations";

        public static void InitializeDatabase()
        {
            string helpersDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Helpers");
            string dbPath = Path.Combine(helpersDirectory, DatabaseFileName);

            // Ensure the Helpers directory exists
            if (!Directory.Exists(helpersDirectory))
            {
                Directory.CreateDirectory(helpersDirectory);
            }

            // Create the database file if it doesn't exist
            if (!File.Exists(dbPath))
            {
                try {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();

                    string createTableQuery = $@"
                        CREATE TABLE IF NOT EXISTS {TableName} (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            SensorId TEXT NOT NULL,
                            Location TEXT NOT NULL,
                            Model TEXT NOT NULL,
                            Brand TEXT NOT NULL,
                            Functionality TEXT NOT NULL,
                            LastMaintained TEXT NOT NULL
                        );";

                    using (var command = new SqliteCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error initializing database: {ex.Message}");
                }
                
            }
        }
    }}