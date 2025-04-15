namespace environmentMonitoring.Views;
using environmentMonitoring.ViewModels;

public partial class HomePage : ContentPage
{
	public HomePage(HomeViewModel homeViewModel)
	{
		this.BindingContext = homeViewModel;
		InitializeComponent();
	}
}