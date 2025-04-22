using System;
using environmentMonitoring.Database.Models;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using environmentMonitoring.Database.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using environmentMonitoring.Services;

namespace environmentMonitoring.ViewModels;

/*! Role View Model class handles the validation and 
 *  calls CRUD operations for roles from the role permissions service class
 *  This view model is used in an observable list, each being a single role object
 */

public partial class RoleViewModel: ObservableObject, IQueryAttributable
{
    private Role _role;
    private readonly RolePermissionService _rpService;

    public string role_type
    {
        get => _role.role_type;
        set
        {
            if (_role.role_type != value)
            {
                _role.role_type = value;
                OnPropertyChanged();
            }
        }
    }

    public int role_Id => _role.role_Id;

    public RoleViewModel(RolePermissionService rolePermissionService)
    {
        _rpService = rolePermissionService;
        _role = new Role(); 
    }


    public RoleViewModel(RolePermissionService rolePermissionService, Role role)
    {
        
        _rpService = rolePermissionService;
        _role = role;
    }


    /*! Save method validates the role type and checks if it already exists in the database
     *  If the role doesn't exists it creates a new role and saves it to the database
     *  Displays error message if there is an issue when attempting to save the role
     */
    [RelayCommand]
    private async Task Save()
    {
        if (string.IsNullOrWhiteSpace(role_type))
        {
            await Shell.Current.DisplayAlert("Field Empty", "Pleaser enter a role name.", "OK");
            return;
        }
        else if(_role.role_Id == 0 && _rpService.RoleExists(_role) || _role.role_Id != 0 && _rpService.RoleExists(_role))
        {
            await Shell.Current.DisplayAlert("Role Exists", "Please enter a different role name.", "OK");
            return;
        }
        else
        {
            bool createUpdate = await Shell.Current.DisplayAlert("Confirm", "Are you sure?", "Yes", "No");
                if (!createUpdate) return;
                    try {
                        if (_role.role_Id == 0)
                        {
                            _rpService.CreateRole(_role);
                        }
                        await Shell.Current.GoToAsync($"..?saved={_role.role_Id}");
                    } catch (Exception)
                    {
                        await Shell.Current.DisplayAlert("Error", "Roles cannot be created or updated at this time", "OK");
                        return;
                    }
        }
    }

    /*! Delete method checks that the role exists before attempting to delete it
     *  Displays error message if there is an issue when attempting to delete the role
     */
    [RelayCommand]
    private async Task Delete()
    {
        if (role_Id != 0) {
            bool confirmation = await Shell.Current.DisplayAlert("Confirm", "Are you sure you want to delete this role?", "Yes", "No");
            if (!confirmation) return;
                try {
                    _rpService.DeleteRole(_role);
                } catch (Exception) {
                    await Shell.Current.DisplayAlert("Error", "There was an error while deleting the role.", "OK");
                    return;
                }

                try {
                    await Shell.Current.GoToAsync($"..?deleted={_role.role_Id}");
                } catch (Exception) {
                    await Shell.Current.DisplayAlert("Error", "Role deleted, error during navigation. Please refresh roles page.", "OK");
                    return;
                }
        }
        else {
            await Shell.Current.DisplayAlert("Error", "Role hasn't been created", "OK");
        }
    }

     /*! IQueryAttributable.ApplyQueryAttributes method retrieves the role from the database using an ID passed in the query
     *  If no ID is passed in the query, a new role is created
     *  @param query The query dictionary containing the ID of the role to be retrieved
     *  Displays error message if there is an issue when attempting to retrieve the role
     */ 
    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("load"))
        {
            try {
                _role = _rpService.GetRoleById(int.Parse(query["load"].ToString()));
            } catch (Exception) {
                Shell.Current.DisplayAlert("Error", "Unable to retrieve the role at this time.", "OK");
                return;
            }
        }
        else
        {
            _role = new Role();
        }
        RefreshProperties();
    }

    /*! Reload method attempts to update the current roles roles properties by re-pulling it from the database
     *  Displays error message if there is an issue when attempting to retrieve the role
     */ 
     public void Reload()
    {
        try {
            _rpService.ReloadRole(_role);
            RefreshProperties();
        } catch (Exception) {
                Shell.Current.DisplayAlert("Error", "Unable to update the page at this time..", "OK");
                return;
            }
    }

    /*! RefreshProperties method refreshes the properties of the role to ensure they are up to date
    *  
    */
     private void RefreshProperties()
    {
        OnPropertyChanged(nameof(role_type));
    }

    /*! NavigateToPermissions method navigates the user to a new page to manage the roles permissions
     *  If the role hasn't been created yet, an alert is displayed to the user
     */ 
    [RelayCommand]
    private async Task NavigateToPermissions()
    {
        if (_role.role_Id == 0)
        {
            await Shell.Current.DisplayAlert("Role hasn't been created", "Please save the role before managing permissions.", "OK");
            return;
        }
        try {
            await Shell.Current.GoToAsync($"{nameof(Views.ManageRolePermissionsPage)}?load={_role.role_Id}");
        }
        catch (Exception) {
            await Shell.Current.DisplayAlert("Error", "Unable to navigate to the permissions page.", "OK");
            return;
        }
    }

}
