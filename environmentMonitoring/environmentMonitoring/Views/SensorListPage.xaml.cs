using environmentMonitoring.Database.Data;
using environmentMonitoring.PartialViews;
using environmentMonitoring.ViewModels;

namespace environmentMonitoring.Views;

public partial class SensorListPage : ContentPage
{
	public SensorListPage(SensorListViewModel vm)
	{
		this.BindingContext = vm;
        InitializeComponent();

        // Represent all RealSensors as a SensorListItem PartialView
		vm.RealSensors()
			.ForEach(rs => Body.Add(new SensorListItem(rs)));
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
