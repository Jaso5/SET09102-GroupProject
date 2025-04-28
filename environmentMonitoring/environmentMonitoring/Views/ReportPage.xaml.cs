using Microsoft.Maui.Controls;

namespace environmentMonitoring.Views
{
    public partial class ReportPage : ContentPage
    {
        public ReportPage(string reportContent)
        {
            InitializeComponent();

            // Check if the reportContent is null or empty
            if (string.IsNullOrEmpty(reportContent))
            {
                // Set default content if the report content is null or empty
                ReportContentLabel.Text = "No report content available.";
            }
            else
            {
                // Otherwise, display the report content
                ReportContentLabel.Text = reportContent;
            }
        }
    }
}



