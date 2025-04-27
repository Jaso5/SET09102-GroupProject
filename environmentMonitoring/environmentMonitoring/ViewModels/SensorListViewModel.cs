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

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private bool isNoData;

    [ObservableProperty]
    private bool isDataAvailable;

    public ICommand ScheduleMaintenanceCommand { get; }

    public SensorListViewModel(EnvironmentAppDbContext dbContext)
    {
        _dbContext = dbContext;
        ScheduleMaintenanceCommand = new AsyncRelayCommand<SensorDisplayModel>(ScheduleMaintenance);

        LoadSensors();
    }

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

    private async Task ScheduleMaintenance(SensorDisplayModel sensor)
    {
        if (sensor != null)
        {
            await Shell.Current.DisplayAlert("Maintenance", $"Scheduled maintenance for Sensor {sensor.r_sensor_Id}.", "OK");
        }
    }
}


