using Microsoft.Data.Sqlite;
using System.IO;

namespace environmentMonitoring.Helpers
{
    public static class DatabaseHelper
    {
        private const string DatabaseFileName = "env.db"; // Name of the database file
        private const string TableName = "SensorConfigurations"; // Name of the table to be created

        public static void InitializeDatabase()
        {
            // Get the path to the Helpers directory
            string helpersDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Helpers");
            // Combine the Helpers directory path with the database file name
            string dbPath = Path.Combine(helpersDirectory, DatabaseFileName);

            // Ensure the Helpers directory exists
            if (!Directory.Exists(helpersDirectory))
            {
                Console.WriteLine("Helpers directory does not exist. Creating it...");
                Directory.CreateDirectory(helpersDirectory);
            }
            else
            {
                Console.WriteLine("Helpers directory already exists.");
            }

            // Create the database file if it doesn't exist
            if (!File.Exists(dbPath))
            {
                Console.WriteLine("Database file does not exist. Creating it...");
                try
                {
                    // Establish a connection to the SQLite database
                    using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                    {
                        connection.Open();
                        Console.WriteLine("Database connection opened successfully.");

                        // SQL query to create the table if it doesn't already exist
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

                        // Execute the query to create the table
                        using (var command = new SqliteCommand(createTableQuery, connection))
                        {
                            command.ExecuteNonQuery();
                            Console.WriteLine($"Table '{TableName}' created successfully (if it didn't already exist).");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log any errors that occur during database initialization
                    Console.WriteLine($"Error initializing database: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Database file already exists. Skipping creation.");
            }
        }
    }
}