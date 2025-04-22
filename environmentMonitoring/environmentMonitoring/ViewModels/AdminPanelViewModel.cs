using System;
using CommunityToolkit.Mvvm.Input;

namespace environmentMonitoring.ViewModels;

public partial class AdminPanelViewModel
{
    [RelayCommand]
    private async Task NavigateToManageRoles()
    {
        // Navigate to the admin panel page
        await Shell.Current.GoToAsync("///ManageRolesPage");
    }
}
