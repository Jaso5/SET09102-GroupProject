using environmentMonitoring.ViewModels;

namespace environmentMonitoring.Views;

public partial class RolePage : ContentPage
{
	public RolePage(RoleViewModel RoleViewModel)
	{
		this.BindingContext = RoleViewModel;
		InitializeComponent();
	}
}