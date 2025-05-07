using CommunityToolkit.Mvvm.Input;
using environmentMonitoring.Database.Data;
using environmentMonitoring.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows.Input;

namespace environmentMonitoring.ViewModels;

public partial class SensorListViewModel
{
    public ICommand BackCommand { get; }
    private EnvironmentAppDbContext dbctx;

    public SensorListViewModel(EnvironmentAppDbContext dbctx)
    {
        this.dbctx = dbctx;

        dbctx.VirtualSensors.ToList();

        BackCommand = new AsyncRelayCommand(NavigateToHomePage);
    }

    [RelayCommand]
    private async Task NavigateToHomePage() => await Shell.Current.GoToAsync(nameof(Views.HomePage));

    /// <summary>
    /// Returns the list of RealSensors from the database
    /// </summary>
    /// <returns></returns>
    internal List<RealSensor> RealSensors() => dbctx
        .RealSensors
        .Include(rs => rs.VirtualSensor)
        .ThenInclude(vs => vs.Quantity)
        .ToList();
}