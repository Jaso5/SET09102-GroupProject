using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Hosting;

namespace environmentMonitoring
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}