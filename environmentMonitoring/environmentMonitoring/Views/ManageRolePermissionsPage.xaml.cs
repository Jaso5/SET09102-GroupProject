using environmentMonitoring.ViewModels;


namespace environmentMonitoring.Views;

public partial class ManageRolePermissionsPage : ContentPage
{
	public ManageRolePermissionsPage(ManageRolePermissionsViewModel ManageRolePermissionsViewModel)
	{
		
		this.BindingContext = ManageRolePermissionsViewModel;
		
		InitializeComponent();
	}
}