using System;
using System;
using environmentMonitoring.Database.Models;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using environmentMonitoring.Database.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using environmentMonitoring.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Diagnostics;

namespace environmentMonitoring.ViewModels;

public partial class ManageRolePermissionsViewModel: ObservableObject, IQueryAttributable
{

    private Role _role;
    

    [ObservableProperty]
    private string role_type;

    private readonly EnvironmentAppDbContext _context;

    [ObservableProperty]
    public ObservableCollection<ViewModels.PermissionViewModel> permissionList;
    
    [ObservableProperty]
    public ObservableCollection<ViewModels.PermissionViewModel>? currentPermissions;

    public ManageRolePermissionsViewModel(EnvironmentAppDbContext context)
    {
        _context = context;
    }

    private async Task populateLists() {
         try {
            PermissionList = new ObservableCollection<ViewModels.PermissionViewModel>(_context.Permissions.ToList().Select(p => new PermissionViewModel(_context, p, _role.role_Id)));

            CurrentPermissions = new ObservableCollection<ViewModels.PermissionViewModel>(_context.RolePermissions
            .Include(r => r.Permissions)
            .Where(r => r.role_Id == _role.role_Id)
            .ToList()
            .Select(p => new PermissionViewModel(_context, p.Permissions, _role.role_Id)));
           
        } catch (Exception) {
                await Shell.Current.GoToAsync($"..");
            }
    }

    private async Task ListCompare() {
        foreach (PermissionViewModel permission in PermissionList)
        {
            if (CurrentPermissions.Any(cp => cp.permission_Id == permission.permission_Id))
            {
                permission.HasPermission = true;
                permission.NoPermission = false;
            }
            else
            {
                permission.HasPermission = false;
                permission.NoPermission = true;
            }
        }
    }

    private async Task InitLists() {

        await populateLists();
        await ListCompare();
        
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("load"))
        {
            try {
                _role = _context.Roles.Single(r => r.role_Id == int.Parse(query["load"].ToString()));
                Role_type = _role.role_type;

                InitLists();
            } catch (Exception) {
                Shell.Current.DisplayAlert("Error", "An error occurred while loading the role.", "OK");
            }
            
        }
    }
}