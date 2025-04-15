namespace environmentMonitoring.Views;
using environmentMonitoring.ViewModels;

public partial class LogInPage : ContentPage
{
	public LogInPage(LoginViewModel LoginViewModel)
	{
		this.BindingContext = LoginViewModel;
		InitializeComponent();
	}
}