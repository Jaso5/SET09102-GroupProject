using Microsoft.Maui.Controls;

namespace environmentMonitoring
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Set SensorConfigPage as the main page
            MainPage = new SensorConfigPage();
        }
    }
}