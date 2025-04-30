using System.Collections.ObjectModel;
using Microsoft.Data.Sqlite;
using Microsoft.Maui.Controls;

namespace environmentMonitoring
{
    public partial class SensorConfigPage : ContentPage
    {
        private ObservableCollection<SensorConfiguration> Sensors { get; set; } = new();
        private SensorConfiguration? selectedSensor;

        public SensorConfigPage()
        {
            InitializeComponent();
            SensorsCollection.ItemsSource = Sensors;
            LoadSensors();
            Helpers.DatabaseHelper.InitializeDatabase();
        }

        /// <summary>
        /// Loads all sensor configurations from the database and populates the ObservableCollection.
        /// </summary>
        private async void LoadSensors()
        {
            try
            {
                string dbPath = Helpers.DatabaseHelper.GetDatabasePath();
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    var command = new SqliteCommand("SELECT * FROM SensorConfigurations", connection);
                    using (var reader = command.ExecuteReader())
                    {
                        Sensors.Clear();
                        while (reader.Read())
                        {
                            Sensors.Add(new SensorConfiguration
                            {
                                Id = reader.GetInt32(0),
                                SensorId = reader.GetString(1),
                                Location = reader.GetString(2),
                                Model = reader.GetString(3),
                                Brand = reader.GetString(4),
                                Functionality = reader.GetString(5),
                                LastMaintained = DateTime.Parse(reader.GetString(6))
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error and display an alert to the user
                Console.WriteLine($"Error loading sensors: {ex.Message}");
                await DisplayAlert("Error", $"Failed to load sensors: {ex.Message}", "OK");
            }
        }

        /// <summary>
        /// Handles the selection of a sensor from the list and populates the input fields.
        /// </summary>
        private void OnSensorSelected(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (e.CurrentSelection.FirstOrDefault() is SensorConfiguration sensor)
                {
                    selectedSensor = sensor;
                    SensorIdEntry.Text = sensor.SensorId;
                    LocationEntry.Text = sensor.Location;
                    ModelEntry.Text = sensor.Model;
                    BrandEntry.Text = sensor.Brand;
                    FunctionalityEntry.Text = sensor.Functionality;
                    LastMaintainedDatePicker.Date = sensor.LastMaintained;
                }
            }
            catch (Exception ex)
            {
                // Log the error and display an alert to the user
                Console.WriteLine($"Error selecting sensor: {ex.Message}");
                DisplayAlert("Error", $"Failed to select sensor: {ex.Message}", "OK");
            }
        }

        /// <summary>
        /// Updates the selected sensor configuration in the database.
        /// </summary>
        private async void OnUpdateConfigurationClicked(object sender, EventArgs e)
        {
            if (selectedSensor == null)
            {
                await DisplayAlert("Error", "Please select a sensor to update", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(SensorIdEntry.Text))
            {
                await DisplayAlert("Error", "Sensor ID cannot be empty or whitespace", "OK");
                return;
            }

            try
            {
                string dbPath = Helpers.DatabaseHelper.GetDatabasePath();
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    string updateQuery = @"
                        UPDATE SensorConfigurations 
                        SET SensorId = @SensorId, Location = @Location, 
                            Model = @Model, Brand = @Brand, 
                            Functionality = @Functionality, LastMaintained = @LastMaintained
                        WHERE Id = @Id";

                    using (var command = new SqliteCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Id", selectedSensor.Id);
                        command.Parameters.AddWithValue("@SensorId", SensorIdEntry.Text);
                        command.Parameters.AddWithValue("@Location", LocationEntry.Text);
                        command.Parameters.AddWithValue("@Model", ModelEntry.Text);
                        command.Parameters.AddWithValue("@Brand", BrandEntry.Text);
                        command.Parameters.AddWithValue("@Functionality", FunctionalityEntry.Text);
                        command.Parameters.AddWithValue("@LastMaintained", LastMaintainedDatePicker.Date.ToString("yyyy-MM-dd"));

                        command.ExecuteNonQuery();
                    }
                }

                await DisplayAlert("Success", "Sensor configuration updated successfully.", "OK");
                ClearFields();
                LoadSensors();
            }
            catch (Exception ex)
            {
                // Log the error and display an alert to the user
                Console.WriteLine($"Error updating sensor: {ex.Message}");
                await DisplayAlert("Error", $"Failed to update sensor: {ex.Message}", "OK");
            }
        }

        /// <summary>
        /// Saves a new sensor configuration to the database.
        /// </summary>
        private async void OnSaveConfigurationClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SensorIdEntry.Text))
            {
                await DisplayAlert("Error", "Sensor ID cannot be empty or whitespace", "OK");
                return;
            }

            try
            {
                var newSensor = new SensorConfiguration
                {
                    SensorId = SensorIdEntry.Text,
                    Location = LocationEntry.Text,
                    Model = ModelEntry.Text,
                    Brand = BrandEntry.Text,
                    Functionality = FunctionalityEntry.Text,
                    LastMaintained = LastMaintainedDatePicker.Date
                };

                string dbPath = Helpers.DatabaseHelper.GetDatabasePath();
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    var command = new SqliteCommand(
                        @"INSERT INTO SensorConfigurations 
                        (SensorId, Location, Model, Brand, Functionality, LastMaintained) 
                        VALUES (@SensorId, @Location, @Model, @Brand, @Functionality, @LastMaintained)",
                        connection);

                    command.Parameters.AddWithValue("@SensorId", newSensor.SensorId);
                    command.Parameters.AddWithValue("@Location", newSensor.Location);
                    command.Parameters.AddWithValue("@Model", newSensor.Model);
                    command.Parameters.AddWithValue("@Brand", newSensor.Brand);
                    command.Parameters.AddWithValue("@Functionality", newSensor.Functionality);
                    command.Parameters.AddWithValue("@LastMaintained", newSensor.LastMaintained.ToString("yyyy-MM-dd"));

                    command.ExecuteNonQuery();
                }

                await DisplayAlert("Success", "Sensor configuration saved successfully.", "OK");
                ClearFields();
                LoadSensors();
            }
            catch (Exception ex)
            {
                // Log the error and display an alert to the user
                Console.WriteLine($"Error saving sensor: {ex.Message}");
                await DisplayAlert("Error", $"Failed to save sensor: {ex.Message}", "OK");
            }
        }

        /// <summary>
        /// Clears all input fields and resets the selected sensor.
        /// </summary>
        private void ClearFields()
        {
            selectedSensor = null;
            SensorIdEntry.Text = string.Empty;
            LocationEntry.Text = string.Empty;
            ModelEntry.Text = string.Empty;
            BrandEntry.Text = string.Empty;
            FunctionalityEntry.Text = string.Empty;
            LastMaintainedDatePicker.Date = DateTime.Now;
        }
    }

    /// <summary>
    /// Represents a sensor configuration entity.
    /// </summary>
    public class SensorConfiguration
    {
        public int Id { get; set; }
        public string? SensorId { get; set; }
        public string? Location { get; set; }
        public string? Model { get; set; }
        public string? Brand { get; set; }
        public string? Functionality { get; set; }
        public DateTime LastMaintained { get; set; }
    }
}