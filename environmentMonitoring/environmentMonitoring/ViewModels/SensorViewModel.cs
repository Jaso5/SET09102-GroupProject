using CommunityToolkit.Mvvm.Input;
using environmentMonitoring.Database.Models;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using environmentMonitoring.Services;

namespace environmentMonitoring.ViewModels;

public partial class SensorViewModel:ObservableObject
{
    public ICommand BackCommand { get; }

    private readonly INavigateToSensor _sensorNav;
    public RealSensor? rs { get; set; }

    public int? sensorId => rs?.r_sensor_Id;

    public double? longitude => rs?.lon;
    public double? latitude => rs?.lat;



    public SensorViewModel(INavigateToSensor sensorNav)
    {
        BackCommand = new AsyncRelayCommand(NavBack);
        _sensorNav = sensorNav;
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

    /*! NavigateToSensor command makes use of the INavigateToSensor interface to open either google maps app, or a browser
     *  Uses the sensor longitude and latitude, to plot a route from the current location
     *  Displays error message if theres an issue attempting to open the browser, or google maps app
     */
    [RelayCommand]
    private async Task NavigateToSensor() {
         try {
            _sensorNav.NavigateToSensor(longitude, latitude);
        } catch (Exception) {
            await Shell.Current.DisplayAlert("Error", "Couldn't load sensor navigation", "OK");
        }
    }

    [RelayCommand]
    /*! RefreshProperties refreshes the information in the report when called 
     */
     private async Task RefreshProperties()
    {
        OnPropertyChanged(nameof(sensorId));
        OnPropertyChanged(nameof(longitude));
        OnPropertyChanged(nameof(latitude));
    }

    
}