namespace environmentMonitoring;
using environmentMonitoring.Views;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute("LogInPage", typeof(LogInPage));
		Routing.RegisterRoute("HomePage", typeof(HomePage));
		Routing.RegisterRoute("AccountPage", typeof(AccountPage));
		Routing.RegisterRoute("AdminPanelPage", typeof(AdminPanelPage));
        Routing.RegisterRoute("RolePage", typeof(RolePage));
		Routing.RegisterRoute("SensorPage", typeof(SensorPage));
		Routing.RegisterRoute("SensorListPage", typeof(SensorListPage));
        Routing.RegisterRoute("ManageRolePermissionsPage", typeof(ManageRolePermissionsPage));
        Routing.RegisterRoute("ManageRolesPage", typeof(ManageRolesPage));
		Routing.RegisterRoute("ListUsersForRoleAssignmentPage", typeof(ListUsersForRoleAssignmentPage));
		Routing.RegisterRoute("AssignRolePage", typeof(AssignRolePage));
		Routing.RegisterRoute("ReportPage", typeof(ReportPage));
	}
}
