using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System.IO;
using Microsoft.Data.Sqlite;
using environmentMonitoring.Helpers;

namespace environmentMonitoring
{
    public partial class SensorConfigPage : ContentPage
    {
        public SensorConfigPage()
        {
            InitializeComponent();
            DatabaseHelper.InitializeDatabase(); // Initialize the database when the page is loaded
        }

        private async void OnSaveConfigurationClicked(object sender, EventArgs e)
        {
            // Retrieve data from the input fields
            string sensorId = SensorIdEntry.Text;
            string location = LocationEntry.Text;
            string model = ModelEntry.Text;
            string brand = BrandEntry.Text;
            string functionality = FunctionalityEntry.Text;
            DateTime lastMaintainedDate = LastMaintainedDatePicker.Date;

            // Validate the input fields
            if (string.IsNullOrWhiteSpace(sensorId) || string.IsNullOrWhiteSpace(location) ||
                string.IsNullOrWhiteSpace(model) || string.IsNullOrWhiteSpace(brand) ||
                string.IsNullOrWhiteSpace(functionality))
            {
                await DisplayAlert("Error", "Please fill in all fields.", "OK");
                return;
            }

            // Save the configuration to the database
            try
            {
                string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "Helpers", "env.db");
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();

                    string insertQuery = $@"
                        INSERT INTO SensorConfigurations 
                        (SensorId, Location, Model, Brand, Functionality, LastMaintained) 
                        VALUES (@SensorId, @Location, @Model, @Brand, @Functionality, @LastMaintained);";

                    using (var command = new SqliteCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@SensorId", sensorId);
                        command.Parameters.AddWithValue("@Location", location);
                        command.Parameters.AddWithValue("@Model", model);
                        command.Parameters.AddWithValue("@Brand", brand);
                        command.Parameters.AddWithValue("@Functionality", functionality);
                        command.Parameters.AddWithValue("@LastMaintained", lastMaintainedDate.ToString("yyyy-MM-dd"));

                        command.ExecuteNonQuery();
                    }
                }

                await DisplayAlert("Success", "Sensor configuration saved successfully.", "OK");

                // Clear the input fields after saving
                SensorIdEntry.Text = string.Empty;
                LocationEntry.Text = string.Empty;
                ModelEntry.Text = string.Empty;
                BrandEntry.Text = string.Empty;
                FunctionalityEntry.Text = string.Empty;
                LastMaintainedDatePicker.Date = DateTime.Now;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to save configuration: {ex.Message}", "OK");
            }
        }
    }
}