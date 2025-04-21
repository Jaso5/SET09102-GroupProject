using environmentMonitoring.ViewModels;

namespace environmentMonitoring.Views;

public partial class AdminPanelPage : ContentPage
{
	public AdminPanelPage(AdminPanelViewModel AdminPanelViewModel)
	{
		this.BindingContext = AdminPanelViewModel;
		InitializeComponent();
	}
}