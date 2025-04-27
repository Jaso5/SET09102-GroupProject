using System;
using Microsoft.Maui.Controls;

namespace environmentMonitoring.Views
{
    public partial class ReportPage : ContentPage
    {
        public ReportPage(string reportContent)
        {
            InitializeComponent();
            // Set the content of the label to the report content
            ReportContentLabel.Text = reportContent;
        }
    }
}

