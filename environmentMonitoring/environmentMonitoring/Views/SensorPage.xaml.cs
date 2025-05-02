using environmentMonitoring.Database.Models;
using environmentMonitoring.ViewModels;
using System.Diagnostics;

namespace environmentMonitoring.Views;

public partial class SensorPage : ContentPage, IQueryAttributable
{
    public SensorPage(SensorViewModel vm) {
        this.BindingContext = vm;
        InitializeComponent();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        SensorViewModel vm = this.BindingContext as SensorViewModel;
        vm.rs = query["realSensor"] as RealSensor;
    }
}