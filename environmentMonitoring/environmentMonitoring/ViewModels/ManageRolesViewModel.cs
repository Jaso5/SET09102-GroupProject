using System;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using environmentMonitoring.Database.Data;
using environmentMonitoring.Services;

namespace environmentMonitoring.ViewModels;


    /*! ManageRolesViewModel manages a list of role objects to keep the UI updated and current
     *  With the current roles in the database
     *   
     */
public partial class ManageRolesViewModel : IQueryAttributable
{


    public ObservableCollection<ViewModels.RoleViewModel> roleList { get; }
    public ICommand NewCommand { get; }

    public ICommand BackCommand { get; }
    public ICommand SelectRoleCommand { get; }
    private readonly RolePermissionService _rpService;

    public ManageRolesViewModel(RolePermissionService rolePermissionService)
    {
        _rpService = rolePermissionService;
        
        roleList = new ObservableCollection<ViewModels.RoleViewModel>(_rpService.GetRoleList().Select(r => new RoleViewModel(_rpService, r)));
        
        BackCommand = new AsyncRelayCommand(BackAsync);
        NewCommand = new AsyncRelayCommand(NewRoleAsync);
        SelectRoleCommand = new AsyncRelayCommand<ViewModels.RoleViewModel>(SelectRoleAsync);
    }

    private async Task BackAsync()
    {
        try {
            await Shell.Current.GoToAsync(nameof(Views.AdminPanelPage));
        } catch (Exception) {
            await Shell.Current.DisplayAlert("Error", "Navigation Error.", "OK");
        }
    }

    /*! NewRoleAsync method navigates to the role page to create a new role
     *  Displays error message if there is an issue when attempting to navigate to the page
     */
    private async Task NewRoleAsync()
    {
        try {
            await Shell.Current.GoToAsync(nameof(Views.RolePage));
        } catch (Exception) {
            await Shell.Current.DisplayAlert("Error", "Navigation Error.", "OK");
        }
    }
       
    /*! SelectRoleAsync method navigates to the role page to edit an existing role
     *  Displays error message if there is an issue when attempting to navigate to the page
     *  @param Takes a a role view model as a parameter containing 
     */
     private async Task SelectRoleAsync(ViewModels.RoleViewModel role)
    {
        if (role != null) {
            try {
                await Shell.Current.GoToAsync($"{nameof(Views.RolePage)}?load={role.role_Id}");
            } catch (Exception) {
                await Shell.Current.DisplayAlert("Error", "Navigation Error.", "OK");
            }
        }
    }



    /*! IQueryAttributable.ApplyQueryAttributes method keeps the list updated and current
     *  by checking if the role is deleted or saved, and if so, it updates the list accordingly
     *  If it isn't found on the list, it is then added to the list
     *  @param The query dictionary containing the action performed and the ID of the role to be checked
     *  Displays error message if there is an issue when attempting to retrieve the role
     */ 
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("deleted"))
        {
            string roleId = query["deleted"].ToString();
            RoleViewModel matchedRole = roleList.Where((r) => r.role_Id == int.Parse(roleId)).FirstOrDefault();

    
            if (matchedRole != null)
                roleList.Remove(matchedRole);
        }
        else if (query.ContainsKey("saved"))
        {
            string roleId = query["saved"].ToString();
            RoleViewModel matchedRole = roleList.Where((r) => r.role_Id == int.Parse(roleId)).FirstOrDefault();

            if (matchedRole != null)
            {
                matchedRole.Reload();
                roleList.Move(roleList.IndexOf(matchedRole), 0);
            }
            else 
            {
                try {
                    var role = _rpService.GetRoleById(int.Parse(roleId));
                    roleList.Insert(0, new RoleViewModel(_rpService, role));
                } catch (Exception) {
                    Shell.Current.DisplayAlert("Error", "Error when trying to update list.", "OK");
                }
            }


        }
    }

}
