using System;
using environmentMonitoring.Database.Data;
using environmentMonitoring.Database.Models;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using environmentMonitoring.Services;

namespace environmentMonitoring.ViewModels;

/*! PermissionViewModel class handles the validation and checks of whether a role should be added or removed
 *  from the role permissions service class
    *  This view model is used in an observable list, each being a single permission object
    *   
    */

public partial class PermissionViewModel : ObservableObject
{
    private readonly RolePermissionService _rpService;
    private readonly Permission _permission;

    private readonly int roleId;



    public PermissionViewModel(RolePermissionService rolePermissionService, Permission permission, int roleId)
    {
        _rpService = rolePermissionService;
        _permission = permission;
        this.roleId = roleId;
    }
    public int permission_Id => _permission.permission_Id;
    public string description => _permission.description;

    [ObservableProperty]
    public bool hasPermission;

    [ObservableProperty]
    public bool noPermission;


    /*! AddPermission method checks whether the role already has the permission
     *  If the role doesn't have the permission it creates a new role permission and saves it to the database
     *  Displays error message if there is an issue when attempting to save the role permission
     *  Refreshes the page upon 
     */
    [RelayCommand]
    private async Task AddPermission()
    {
        if (!_rpService.RoleHasPermission(roleId, permission_Id)) {
            bool add = await Shell.Current.DisplayAlert("Confirm", "Are you sure you want to add this permission?", "Yes", "No");
                if (!add) return;
                    try {
                        _rpService.AddPermission(new RolePermissions
                        {
                            role_Id = roleId, 
                            permission_Id = permission_Id
                        });
                    } catch (Exception) {
                        await Shell.Current.DisplayAlert("Error", "There was an error while adding the permission.", "OK");
                        return;
                    }
                    await Shell.Current.GoToAsync($"{nameof(Views.ManageRolePermissionsPage)}?load={roleId}");
        }
        
    }

    /*! RemovePermission method checks whether the role has the permission
     *  If the role has the permission, it is fetched and 
     *  Displays error message if there is an issue when attempting to remove the role permission
     */
    [RelayCommand]
    private async Task RemovePermission()
    {
        if (_rpService.RoleHasPermission(roleId, permission_Id)) {
            bool remove = await Shell.Current.DisplayAlert("Confirm", "Are you sure you want to remove this permission?", "Yes", "No");
                if (!remove) return;
                    try {
                        var permission = _rpService.GetRolePermissionById(roleId, permission_Id);
                        _rpService.RemovePermission(permission);
                    } catch (Exception) {
                        await Shell.Current.DisplayAlert("Error", "There was an error while removing the permission.", "OK");
                        return;
                    }       
                    await Shell.Current.GoToAsync($"{nameof(Views.ManageRolePermissionsPage)}?load={roleId}");
        }
        
    }

}
