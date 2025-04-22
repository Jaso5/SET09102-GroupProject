using environmentMonitoring.ViewModels;

namespace environmentMonitoring.Views;

public partial class AddNewRolePage : ContentPage
{
	public AddNewRolePage(AddNewRoleViewModel AddNewRoleViewModel)
	{
		this.BindingContext = AddNewRoleViewModel;
		InitializeComponent();
	}
}