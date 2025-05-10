using CommunityToolkit.Mvvm.Input;
using environmentMonitoring.Database.Models;
using System.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace environmentMonitoring.ViewModels;

public partial class SensorViewModel:ObservableObject
{
    public ICommand BackCommand { get; }
    public RealSensor? rs { get; set; }

    public int? sensorId => rs.r_sensor_Id;

    public SensorViewModel()
    {
        BackCommand = new AsyncRelayCommand(NavBack);
    }

    [RelayCommand]
    private async Task NavBack() => await Shell.Current.GoToAsync("..");

    /*! CreateIncidentReport command navigates the user to the incident report page
     *  Passes the sensor ID as a query parameter during navigation
     *  Displays error message if theres an issue during navigation
     */
    [RelayCommand]
    private async Task CreateIncidentReport() {
         try {
            await Shell.Current.GoToAsync($"{nameof(Views.IncidentReportEditPage)}?new={rs.r_sensor_Id}");
        } catch (Exception) {
            await Shell.Current.DisplayAlert("Error", "Navigation Error.", "OK");
        }
    }
}





     /*! RefreshProperties refreshes the information in the report when called 
     */
     [RelayCommand]
     private async Task RefreshProperties()
    {
        OnPropertyChanged(nameof(sensorId));
        /*
        OnPropertyChanged(nameof(reportDateTime));
        OnPropertyChanged(nameof(incidentType));
        OnPropertyChanged(nameof(incidentStatus));
        OnPropertyChanged(nameof(reportLastUpdateDateTime));
        OnPropertyChanged(nameof(incidentDescription));
        OnPropertyChanged(nameof(incidentNextSteps));
        OnPropertyChanged(nameof(incidentResolution));
        */
    }

}