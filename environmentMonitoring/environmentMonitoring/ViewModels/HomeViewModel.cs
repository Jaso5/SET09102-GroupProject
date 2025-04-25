using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage; // Needed for Preferences

namespace environmentMonitoring.ViewModels;

/*! HomeViewModel is the view model for the home page, handles basic navigation
 */

// Create commands to link to other pages.
// Block access to pages depending on the user's role
public partial class HomeViewModel
{

    // Navigation to Admin Panel
    [RelayCommand]
    private async Task NavigateToAdminPanel()
    {
        int roleId = Preferences.Get("role_id", 0); // Default to 0 (non-admin)

        // If user is admin(roleId = 1) allow to pass to the page
        if (roleId == 1)
        {
            try
            {
                await Shell.Current.GoToAsync("///AdminPanelPage");
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert("Error", "Error during navigation", "OK");
            }
        }
        // If user role is anything else shoot out a error message
        else
        {
            await Shell.Current.DisplayAlert("Access Denied", "You must be an admin to access this page.", "OK");
        }
    }

    // Navigation to Account Page
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

    // Navigation to Sensor Page
    [RelayCommand]
    private async Task NavigateToSensorPage()
    {
        int roleId = Preferences.Get("role_id", 0);

        // If the users role is admin, environment scientist or operations manager, allow them to pass to page
        if (roleId == 1 || roleId == 2 || roleId == 3)
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
        // Otherwise send out error message
        else
        {
            await Shell.Current.DisplayAlert("Access Denied", "You do not have permission to access the Sensor Page.", "OK");
        }
    }


    // Navigation to Sensor List Page
    [RelayCommand]
    private async Task NavigateToSensorListPage()
    {
        int roleId = Preferences.Get("role_id", 0);

        // If the users role is admin, environment scientist or operations manager, allow them to pass to page
        if (roleId == 1 || roleId == 2 || roleId == 3)
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
        // Otherwise send out error message
        else
        {
            await Shell.Current.DisplayAlert("Access Denied", "You do not have permission to access the Sensor Page.", "OK");
        }
    }
}

