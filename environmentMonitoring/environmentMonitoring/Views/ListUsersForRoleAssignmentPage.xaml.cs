using environmentMonitoring.ViewModels;

namespace environmentMonitoring.Views;

public partial class ListUsersForRoleAssignmentPage : ContentPage
{
	public ListUsersForRoleAssignmentPage(ListUsersForRoleAssignmentViewModel listUsersForRoleAssignmentViewModel)
	{
		this.BindingContext = listUsersForRoleAssignmentViewModel;
		InitializeComponent();
	}
}