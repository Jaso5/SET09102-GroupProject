using CommunityToolkit.Mvvm.Input;
using environmentMonitoring.Database.Models;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace environmentMonitoring.ViewModels;

public partial class SensorViewModel:ObservableObject
{
    public ICommand BackCommand { get; }
    public RealSensor? rs { get; set; }

    public int? sensorId => rs?.r_sensor_Id;

    public float? longitude => rs?.lon;
    public float? latitude => rs?.lat;



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

    /*! NavigateToSensor command navigates opens either google maps app, or a browser
     *  Uses the sensor longitude and latitude, to plot a route from the current location
     *  Displays error message if theres an issue attempting to open the browser, or google maps app
     */
    [RelayCommand]
    private async Task NavigateToSensor() {
         try {
            Uri uri = new Uri("https://www.google.com/maps/dir/?api=1&destination="+latitude +"+"+ longitude+"&travelmode=driving");
            BrowserLaunchOptions options = new BrowserLaunchOptions()
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show,
                PreferredToolbarColor = Colors.Violet,
                PreferredControlColor = Colors.SandyBrown
            };

            await Browser.Default.OpenAsync(uri, options);

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