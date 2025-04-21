using System;
using environmentMonitoring.Database.Data;
using environmentMonitoring.Database.Models;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace environmentMonitoring.ViewModels;

public partial class PermissionViewModel : ObservableObject
{
    private readonly EnvironmentAppDbContext _context;
    private readonly Permission _permission;

    private readonly int roleId;



    public PermissionViewModel(EnvironmentAppDbContext context, Permission permission, int roleId)
    {
        _context = context;
        _permission = permission;
        this.roleId = roleId;
    }
    public int permission_Id => _permission.permission_Id;
    public string description => _permission.description;

    [ObservableProperty]
    public bool hasPermission;

    [ObservableProperty]
    public bool noPermission;

    [RelayCommand]
    private async Task AddPermission()
    {
        if (!RoleHasPermission()){
            bool add = await Shell.Current.DisplayAlert("Confirm", "Are you sure you want to add this permissions?", "Yes", "No");
                if (!add) return;
                    try {
                        _context.RolePermissions.Add(new RolePermissions
                        {
                            role_Id = roleId, 
                            permission_Id = permission_Id
                        });

                        await _context.SaveChangesAsync();
                        await Shell.Current.GoToAsync($"{nameof(Views.ManageRolePermissionsPage)}?load={roleId}");
                    } catch (Exception) {
                        await Shell.Current.DisplayAlert("Error", "An error occurred while adding the permission.", "OK");
                    }
        }
        
    }

    [RelayCommand]
    private async Task RemovePermission()
    {
        if (roleId != 0 && permission_Id != 0) {
            bool remove = await Shell.Current.DisplayAlert("Confirm", "Are you sure you want to remove this permissions?", "Yes", "No");
                try {
                    if (!remove) return;
                        var removePermission = _context.RolePermissions.FirstOrDefault(r => r.permission_Id == permission_Id && r.role_Id == roleId);
                            if (removePermission != null)
                                _context.RolePermissions.Remove(removePermission);

                                await _context.SaveChangesAsync();
                                await Shell.Current.GoToAsync($"{nameof(Views.ManageRolePermissionsPage)}?load={roleId}");
                    } catch (Exception) {
                        await Shell.Current.DisplayAlert("Error", "An error occurred while removing the permission.", "OK");
                    }       
        }
        
    }

    private bool RoleHasPermission()
    {   if (roleId == 0 || permission_Id == 0) return false;
            return _context.RolePermissions.Any(r => r.permission_Id == _permission.permission_Id && r.role_Id == roleId);
    }
}
