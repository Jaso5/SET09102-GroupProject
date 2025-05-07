using environmentMonitoring.Database.Data;
using environmentMonitoring.Database.Models;
using environmentMonitoring.PartialViews;
using environmentMonitoring.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace environmentMonitoring.Views;

public partial class SensorPage : ContentPage, IQueryAttributable
{
    private readonly EnvironmentAppDbContext _dbContext; // Database context
    string generatedReportPath; // Store the generated report path
 
    public SensorPage(SensorViewModel vm, EnvironmentAppDbContext dbContext) {
        this.BindingContext = vm;
        _dbContext = dbContext;
        InitializeComponent();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        // Grab the realsensor selected in the last page
        SensorViewModel vm = this.BindingContext as SensorViewModel;
        vm.rs = query["realSensor"] as RealSensor;

        // Take all Virtual Sensors and convert them to the corresponding view
        vm.rs.VirtualSensor
            .Select(vs => new VirtualSensorView(vs, _dbContext))
            .ToList()
            .ForEach(vsv => SensorsList.Add(vsv));
    }

    // This method is triggered when the 'Generate Report' button is clicked
    private async void OnGenerateReportClicked(object sender, EventArgs e)
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

            // Fetch actual data from the database (for example, trends from dbo.Reports)
            var reportsData = await _dbContext.Reports.ToListAsync(); // Fetch all reports

            // If no reports found, display a message
            if (reportsData == null || reportsData.Count == 0)
            {
                ReportMessageLabel.Text = "No environmental data available to generate the report.";
                return;
            }

            // Generate the content for the report (including trends and other info)
            string reportContent = "Environmental Trend Report\n\nGenerated on: " + DateTime.Now.ToString();
            reportContent += "\n\n--- Trend Data ---\n";

            // Loop through fetched reports and add their details to the report content
            foreach (var report in reportsData)
            {
                reportContent += $"\nTitle: {report.title}\nTrend: {report.trend}\nSensor ID: {report.v_sensor_id}\nBody: {report.body}\n";
                reportContent += "-----------------------------------\n";
            }

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

                // Ensure the content is not null or empty before passing to ReportPage
                if (string.IsNullOrEmpty(reportContent))
                {
                    await DisplayAlert("Error", "The report is empty.", "OK");
                }
                else
                {
                    // Navigate to ReportPage and pass the report content (string)
                    await Navigation.PushAsync(new ReportPage(reportContent));
                }
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