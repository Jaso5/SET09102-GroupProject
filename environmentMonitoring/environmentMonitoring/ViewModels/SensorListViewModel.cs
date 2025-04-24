using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace environmentMonitoring.ViewModels;

public partial class SensorListViewModel
{
    public ICommand BackCommand { get; }

    public SensorListViewModel()
    {

        BackCommand = new AsyncRelayCommand(NavigateToHomePage);
    }

    [RelayCommand]
    private async Task NavigateToHomePage()
    {
        await Shell.Current.GoToAsync("///HomePage");
    }

}