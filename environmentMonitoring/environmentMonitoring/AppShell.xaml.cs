namespace environmentMonitoring;
using environmentMonitoring.Views;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute("LogInPage", typeof(LogInPage));
		Routing.RegisterRoute("HomePage", typeof(HomePage));
		Routing.RegisterRoute("AdminPanelPage", typeof(AdminPanelPage));
        Routing.RegisterRoute("RolePage", typeof(RolePage));
        Routing.RegisterRoute("ManageRolePermissionsPage", typeof(ManageRolePermissionsPage));
        Routing.RegisterRoute("ManageRolesPage", typeof(ManageRolesPage));
	}
}
