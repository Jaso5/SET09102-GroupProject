using CommunityToolkit.Mvvm.Input;
using environmentMonitoring.Database.Models;
using System.Diagnostics;
using System.Windows.Input;

namespace environmentMonitoring.ViewModels;

public partial class SensorViewModel
{
    public ICommand BackCommand { get; }
    public RealSensor? rs { get; set; }

    public SensorViewModel()
    {
        BackCommand = new AsyncRelayCommand(NavBack);
    }

    [RelayCommand]
    private async Task NavBack() => await Shell.Current.GoToAsync("..");

    [RelayCommand]
    private async Task CreateIncidentReport() {
        Debug.WriteLine(rs.r_sensor_Id);
         try {
            await Shell.Current.GoToAsync($"{nameof(Views.IncidentReportEditPage)}?new={rs.r_sensor_Id}");
        } catch (Exception) {
            await Shell.Current.DisplayAlert("Error", "Navigation Error.", "OK");
        }
    }
}


