using CommunityToolkit.Mvvm.Input;
using environmentMonitoring.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows.Input;

namespace environmentMonitoring.Views;

public partial class VirtualSensorSettings : ContentPage, IQueryAttributable
{
    private VirtualSensor? vs;
    public ICommand BackCommand { get; }

    public VirtualSensorSettings()
	{
        BackCommand = new AsyncRelayCommand(NavigateToSensor);
        InitializeComponent();
	}

    [RelayCommand]
    private async Task NavigateToSensor() => await Shell.Current.GoToAsync("..");

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        // Grab the VirtualSensor selected in the last page
        this.vs = query["virtualSensor"] as VirtualSensor;
    }
}