using environmentMonitoring.ViewModels;

namespace environmentMonitoring.Views;

public partial class HomePage : ContentPage
{
	public HomePage(HomeViewModel homeViewModel)
	{
		this.BindingContext = homeViewModel;
		InitializeComponent();
	}
}