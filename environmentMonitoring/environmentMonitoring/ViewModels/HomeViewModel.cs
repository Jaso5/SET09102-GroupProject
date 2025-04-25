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
        // Pull the user's role from:
        // Preferences - Local, non-encrypted value
        // SecureStorage - Encrypted value
        int roleIdFromPrefs = Preferences.Get("role_id", 0);
        string roleIdFromStorage = await SecureStorage.GetAsync("userRoleId");

        // Check both sources for the admin(1 value)
        if (roleIdFromPrefs == 1 || roleIdFromStorage == "1")
        {
            try
            {
                // Directs to the Admin Panel Page
                await Shell.Current.GoToAsync("///AdminPanelPage");
            }
            catch (Exception ex)
            {
                // Display error if navigation is wrong or doesn't exist
                await Shell.Current.DisplayAlert("Error", $"Navigation error: {ex.Message}", "OK");
            }
        }
        else
        {
            // Display error if user doesn't have the correct permissions
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

