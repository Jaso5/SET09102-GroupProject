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

/*! ManageRolesViewModel populates a list of all permissions and a list of current permissions for a specified role
*   it then compares the two lists and sets the hasPermission and noPermission properties of each permission object
*   based on whether the role has the permission currently
*/

public partial class ManageRolePermissionsViewModel: ObservableObject, IQueryAttributable
{

    private Role _role;
    

    [ObservableProperty]
    private string role_type;

     private readonly RolePermissionService _rpService;

    [ObservableProperty]
    public ObservableCollection<ViewModels.PermissionViewModel> permissionList;
    
    [ObservableProperty]
    public ObservableCollection<ViewModels.PermissionViewModel>? currentPermissions;

    public ManageRolePermissionsViewModel(RolePermissionService rolePermissionService)
    {
       _rpService = rolePermissionService;;
    }

    /*! populateLists populates the permissions list and current permissions list
    *   Displays an error message if there is an issue gathering the permissions
    *   
    */
    private async Task populateLists() {
         try {
            PermissionList = new ObservableCollection<ViewModels.PermissionViewModel>(_rpService.GetPermissionsList()
            .Select(p => new PermissionViewModel(_rpService, p, _role.role_Id)));

            CurrentPermissions = new ObservableCollection<ViewModels.PermissionViewModel>(_rpService.GetRolesCurrentPermissions(_role.role_Id)
            .Select(p => new PermissionViewModel(_rpService, p.Permissions, _role.role_Id)));
           
        } catch (Exception) {
                await Shell.Current.DisplayAlert("Error", "There was an error while gathering the permissions.", "OK");
                return;
            }
    }

    /*! ListCompare method compares the 2 lists of permissions and sets the hasPermission and noPermission properties
    *   inside each permission object of PermissionsList based on whether the role has the permission currently 
    *   
    */
    private async Task ListCompare() {
        foreach (PermissionViewModel permission in PermissionList)
        {
            try {
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
            } catch (Exception) {
                await Shell.Current.DisplayAlert("Error", "There was an error while gathering the permissions.", "OK");
                return;
            }
        }
    }

    /*! InitLists method initializes the lists by calling populateLists and ListCompare methods
    *     
    */
    private async Task InitLists() {

        await populateLists();
        await ListCompare();
        
    }

    /*! IQueryAttributable.ApplyQueryAttributes method retrieves the role from the database using an ID passed in the query
     *  Displays error message if there is an issue when attempting to retrieve the role
     *  @param A dictionary containing the ID of the role to be retrieved
     */ 
    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("load"))
        {
            try {
                _role = _rpService.GetRoleById(int.Parse(query["load"].ToString()));
                Role_type = _role.role_type;

                InitLists();
            } catch (Exception) {
                Shell.Current.DisplayAlert("Error", "There was an error when retrieving the role.", "OK");
                return;
            }
            
        }
    }
}