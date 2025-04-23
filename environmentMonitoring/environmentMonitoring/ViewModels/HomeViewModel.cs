using CommunityToolkit.Mvvm.Input;

namespace environmentMonitoring.ViewModels;

/*! HomeViewModel is the view model for the home page, handles basic navigation
     *  
     */ 

public partial class HomeViewModel
{

    [RelayCommand]
    private async Task NavigateToAdminPanel()
    {
        try {
            await Shell.Current.GoToAsync("///AdminPanelPage");
        } catch(Exception) {
            await Shell.Current.DisplayAlert("Error", "Error during navigation", "OK");
        }
    }

    [RelayCommand]
    private async Task NavigateToAccountPage()
    {
        try
        {
            await Shell.Current.GoToAsync("///AccountPage");
        }
        catch (Exception)
        {
            await Shell.Current.DisplayAlert("Error", "Error during navigation", "OK");
        }
    }

    [RelayCommand]
    private async Task NavigateToSensorPage()
    {
        try
        {
            await Shell.Current.GoToAsync("///SensorPage");
        }
        catch (Exception)
        {
            await Shell.Current.DisplayAlert("Error", "Error during navigation", "OK");
        }
    }

    [RelayCommand]
    private async Task NavigateToSensorListPage()
    {
        try
        {
            await Shell.Current.GoToAsync("///SensorListPage");
        }
        catch (Exception)
        {
            await Shell.Current.DisplayAlert("Error", "Error during navigation", "OK");
        }
    }

}
