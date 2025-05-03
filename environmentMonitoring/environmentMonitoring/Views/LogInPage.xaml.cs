using environmentMonitoring.ViewModels;

namespace environmentMonitoring.Views;


public partial class LogInPage : ContentPage
{
	public LogInPage(LoginViewModel LoginViewModel)
	{
		this.BindingContext = LoginViewModel;
		InitializeComponent();
	}
}