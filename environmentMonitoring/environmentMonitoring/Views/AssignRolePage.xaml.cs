using environmentMonitoring.ViewModels;

namespace environmentMonitoring.Views;


public partial class AssignRolePage : ContentPage
{
	public AssignRolePage(AssignRoleViewModel assignRoleViewModel)
	{
		this.BindingContext = assignRoleViewModel;
		InitializeComponent();
	}
}