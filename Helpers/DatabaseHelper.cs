using Microsoft.Data.Sqlite;
using System.IO;

namespace environmentMonitoring.Helpers
{
    public static class DatabaseHelper
    {
        private const string DatabaseFileName = "env.db";
        private const string TableName = "SensorConfigurations";

        public static string GetDatabasePath()
        {
            string appDataPath = FileSystem.AppDataDirectory;
            return Path.Combine(appDataPath, DatabaseFileName);
        }

        public static void InitializeDatabase()
        {
            string dbPath = GetDatabasePath();
            
            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();

                    var command = new SqliteCommand(
                        $@"CREATE TABLE IF NOT EXISTS {TableName} (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            SensorId TEXT NOT NULL,
                            Location TEXT NOT NULL,
                            Model TEXT NOT NULL,
                            Brand TEXT NOT NULL,
                            Functionality TEXT NOT NULL,
                            LastMaintained TEXT NOT NULL
                        );", connection);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database initialization error: {ex.Message}");
                throw;
            }
        }
    }
}