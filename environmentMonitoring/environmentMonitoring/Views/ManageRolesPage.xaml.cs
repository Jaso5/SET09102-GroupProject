using environmentMonitoring.ViewModels;

namespace environmentMonitoring.Views;

public partial class ManageRolesPage : ContentPage
{
	public ManageRolesPage(ManageRolesViewModel ManageRolesViewModel)
	{
		this.BindingContext = ManageRolesViewModel;
		InitializeComponent();
	}

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        roleList.SelectedItem = null;
    }
}