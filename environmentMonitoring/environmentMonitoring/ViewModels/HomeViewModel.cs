using Microsoft.Maui.Storage;
using environmentMonitoring.Services;
using CommunityToolkit.Mvvm.Input;
using environmentMonitoring.Database.Data;

namespace environmentMonitoring.ViewModels
{
    public partial class HomeViewModel
    {
        private readonly PermissionService _permissionService;
        private readonly EnvironmentAppDbContext _dbContext;

        public HomeViewModel(PermissionService permissionService, EnvironmentAppDbContext dbContext)
        {
            _permissionService = permissionService;
            _dbContext = dbContext;
        }

        // Navigation to Admin Panel
        [RelayCommand]
        private async Task NavigateToAdminPanel()
        {
            int roleIdFromPrefs = Preferences.Get("role_id", 0);
            string roleIdFromStorage = await SecureStorage.GetAsync("userRoleId");

            // Compare all known sources
            if (roleIdFromPrefs == 1 || roleIdFromStorage == "1")
            {
                // Check permissions 9 and 10 for Admin Panel
                bool hasPermissions = await _permissionService.HasPermissionAsync(9, int.Parse(roleIdFromStorage)) &&
                                      await _permissionService.HasPermissionAsync(10, int.Parse(roleIdFromStorage));

                if (hasPermissions)
                {
                    try
                    {
                        await Shell.Current.GoToAsync("///AdminPanelPage");
                    }
                    catch (Exception ex)
                    {
                        await Shell.Current.DisplayAlert("Error", $"Navigation error: {ex.Message}", "OK");
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Access Denied",
                        "You do not have the necessary permissions to access this page.",
                        "OK");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Access Denied",
                    $"You must be an admin to access this page. [Prefs: {roleIdFromPrefs}, Secure: {roleIdFromStorage}]",
                    "OK");
            }
        }

        // Navigation to Sensor Page
        [RelayCommand]
        private async Task NavigateToSensorPage()
        {
            int roleIdFromStorage = int.Parse(await SecureStorage.GetAsync("userRoleId"));

            // Check permission 6 for Sensor Page
            bool hasPermissions = await _permissionService.HasPermissionAsync(6, roleIdFromStorage);

            if (hasPermissions)
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
            else
            {
                await Shell.Current.DisplayAlert("Access Denied", "You do not have permission to access the Sensor Page.", "OK");
            }
        }

        // Navigation to Sensor List Page
        [RelayCommand]
        private async Task NavigateToSensorListPage()
        {
            int roleIdFromStorage = int.Parse(await SecureStorage.GetAsync("userRoleId"));

            // Check permission 6 for Sensor List Page
            bool hasPermissions = await _permissionService.HasPermissionAsync(6, roleIdFromStorage);

            if (hasPermissions)
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
            else
            {
                await Shell.Current.DisplayAlert("Access Denied", "You do not have permission to access the Sensor List Page.", "OK");
            }
        }
    }
}


