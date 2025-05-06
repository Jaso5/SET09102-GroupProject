using CommunityToolkit.Mvvm.Input;
using environmentMonitoring.Database.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Windows.Input;

namespace environmentMonitoring.ViewModels;

/*! AdminPanelViewModel navigates to various Admin only locations
     *
     */ 

public partial class AdminPanelViewModel
{
    public ICommand BackCommand { get; }

    public string BackupName { get; set;}

    private readonly EnvironmentAppDbContext dbctx;

    public AdminPanelViewModel(EnvironmentAppDbContext dbctx)
    {
        BackCommand = new AsyncRelayCommand(NavigateToHomePage);
        this.dbctx = dbctx;
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

    [RelayCommand]
    public async Task BackupDatabase()
    {
        // Sql injection? How do you fit a query into a syringe?
        // We can't use sanitised inputs because for some reason it fails to actually substitute in the value.
        await dbctx.Database.ExecuteSqlRawAsync($"BACKUP DATABASE EnvMon TO DISK = 'backup/{this.BackupName}';");
    }

    [RelayCommand]
    public async Task RestoreDatabase()
    {

        // This fails as we need to disconnect our session and reconnect from the master database. Ultimately this should only happen in emergencies
        // so using Azure Data Studio or any other query runner is acceptable.
        await dbctx.Database.ExecuteSqlRawAsync($"RESTORE DATABASE EnvMon FROM DISK = 'backup/{this.BackupName}';");
    }
}
