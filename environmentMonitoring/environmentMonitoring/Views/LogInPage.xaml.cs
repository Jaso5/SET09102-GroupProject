namespace environmentMonitoring.Views;
using environmentMonitoring.ViewModels;

public partial class LogInPage : ContentPage
{
	public LogInPage()
	{
		this.BindingContext = new LoginViewModel();
		InitializeComponent();
	}
}