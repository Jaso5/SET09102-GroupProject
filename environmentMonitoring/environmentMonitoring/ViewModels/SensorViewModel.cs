using CommunityToolkit.Mvvm.Input;
using environmentMonitoring.Database.Models;
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
}