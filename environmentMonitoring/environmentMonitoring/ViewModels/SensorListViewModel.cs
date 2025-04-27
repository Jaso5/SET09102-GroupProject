using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using environmentMonitoring.Database.Data;
using environmentMonitoring.Database.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace environmentMonitoring.ViewModels;

public partial class SensorListViewModel : ObservableObject
{
    private readonly EnvironmentAppDbContext _dbContext;

    [ObservableProperty]
    private ObservableCollection<SensorDisplayModel> sensors = new();

    // Variables that check if the sensor list page is Loading and if page has Data

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private bool isNoData;

    [ObservableProperty]
    private bool isDataAvailable;

    // Command to allow the user to set a Maintenance date for sensors

    public ICommand ScheduleMaintenanceCommand { get; }

    public SensorListViewModel(EnvironmentAppDbContext dbContext)
    {
        _dbContext = dbContext;
        ScheduleMaintenanceCommand = new AsyncRelayCommand<SensorDisplayModel>(ScheduleMaintenance);

        LoadSensors();
    }

    // Load Sensor data from the database
    private async void LoadSensors()
    {
        isLoading = true;
        isDataAvailable = false;
        isNoData = false;

        try
        {
            var realSensors = await _dbContext.RealSensors.ToListAsync();
            Sensors.Clear();

            if (realSensors.Any())
            {
                foreach (var sensor in realSensors)
                {
                    Sensors.Add(new SensorDisplayModel(sensor));
                }
                IsDataAvailable = true;
            }
            else
            {
                IsNoData = true;
            }
        }
        catch (Exception ex)
        {
            // Handle any errors (e.g., display an alert or log)
        }
        finally
        {
            IsLoading = false;
        }
    }

    // Schedule the maintenance date for the inactive sensors

    private async Task ScheduleMaintenance(SensorDisplayModel sensor)
    {
        if (sensor != null && sensor.MaintenanceDate.HasValue)
        {
            var maintenanceDate = sensor.MaintenanceDate.Value;

            // Perform scheduling logic (you can store the maintenance date in your database if needed)

            await Shell.Current.DisplayAlert("Maintenance",
                $"Scheduled maintenance for Sensor {sensor.r_sensor_Id} on {maintenanceDate.ToShortDateString()}.",
                "OK");
        }
        else
        {
            // If the user hasn't selected a date, show an error message
            await Shell.Current.DisplayAlert("Error",
                "Please select a valid maintenance date.",
                "OK");
        }
    }

}


