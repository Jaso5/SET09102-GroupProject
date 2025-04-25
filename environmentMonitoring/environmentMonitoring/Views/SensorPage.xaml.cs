using Microsoft.Maui.Storage; // For Preferences

namespace environmentMonitoring.Views;

public partial class SensorPage : ContentPage
{
    public SensorPage()
    {
        InitializeComponent();
    }

    // Redirect any user that doesn't have the required permissions to homepage
    protected override void OnAppearing()
    {
        base.OnAppearing();

        int roleId = Preferences.Get("role_id", 0); // Default to 0 (unauthorized)

        if (roleId != 1 && roleId != 2 && roleId != 3)
        {
            Shell.Current.DisplayAlert("Access Denied", "You do not have permission to view this page.", "OK");
            Shell.Current.GoToAsync("///HomePage"); // Adjust if needed
        }
    }
}
