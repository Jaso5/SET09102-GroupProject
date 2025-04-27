using System;
using System.IO;
using Microsoft.Maui.Controls;

namespace environmentMonitoring.Views
{
    public partial class SensorPage : ContentPage
    {
        string generatedReportPath; // Store the generated report path

        public SensorPage()
        {
            InitializeComponent();
        }

        // This method is triggered when the 'Generate Report' button is clicked
        private void OnGenerateReportClicked(object sender, EventArgs e)
        {
            try
            {
                // Get the path for the 'Reports' folder inside the Documents directory
                string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Reports");

                // Check if the directory exists, if not, create it
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Define the file path for the report
                generatedReportPath = Path.Combine(folderPath, "EnvironmentalTrendReport.txt");

                // Example content for the report
                string reportContent = "Environmental Trend Report\n\nGenerated on: " + DateTime.Now.ToString();

                // Write content to the file
                File.WriteAllText(generatedReportPath, reportContent);

                // Update the Label to show the file location or success message
                ReportMessageLabel.Text = $"Report successfully generated! \nFile saved at: {generatedReportPath}";

                // Show the Open Report button
                OpenReportButton.IsVisible = true;
            }
            catch (Exception ex)
            {
                // Handle any errors during the file generation
                ReportMessageLabel.Text = $"An error occurred while generating the report: {ex.Message}";
            }
        }

        // This method is triggered when the 'Open Report' button is clicked
        private async void OnOpenReportClicked(object sender, EventArgs e)
        {
            try
            {
                // Ensure the file exists
                if (File.Exists(generatedReportPath))
                {
                    // Read the content of the report
                    string reportContent = File.ReadAllText(generatedReportPath);

                    // Navigate to ReportPage and pass the report content
                    await Navigation.PushAsync(new ReportPage(reportContent));
                }
                else
                {
                    await DisplayAlert("Error", "Report file not found.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred while opening the report: {ex.Message}", "OK");
            }
        }
    }
}

