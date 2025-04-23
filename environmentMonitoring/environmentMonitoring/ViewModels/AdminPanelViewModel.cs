using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace environmentMonitoring.ViewModels;

/*! AdminPanelViewModel navigates to various Admin only locations
     *
     */ 

public partial class AdminPanelViewModel
{
    public ICommand BackCommand { get; }

    public AdminPanelViewModel()
    {
        
        BackCommand = new AsyncRelayCommand(NavigateToHomePage);
    }

    [RelayCommand]
    private async Task NavigateToManageRoles()
    {
        await Shell.Current.GoToAsync("///ManageRolesPage");
    }

     
    [RelayCommand]
    private async Task NavigateToListUsersAssignRole()
    {
        
        await Shell.Current.GoToAsync("///ListUsersForRoleAssignmentPage");
    }

    [RelayCommand]
    private async Task NavigateToHomePage()
    {     
        await Shell.Current.GoToAsync("///HomePage");
    }
}
