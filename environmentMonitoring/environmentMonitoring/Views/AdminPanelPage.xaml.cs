using Microsoft.Maui.Storage; // For Preferences
using environmentMonitoring.ViewModels;
namespace environmentMonitoring.Views;

public partial class AdminPanelPage : ContentPage
{
    public AdminPanelPage(AdminPanelViewModel AdminPanelViewModel)
    {
        this.BindingContext = AdminPanelViewModel;
        InitializeComponent();
    }

    // Redirect any user that isn't admin to homepage
    protected override void OnAppearing()
    {
        base.OnAppearing();

        int roleId = Preferences.Get("role_id", 0); 
        if (roleId != 1)
        {
            Shell.Current.DisplayAlert("Access Denied", "You must be an admin to view this page.", "OK");
            Shell.Current.GoToAsync("///HomePage"); 
        }
    }
}
