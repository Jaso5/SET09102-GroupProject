using System;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using environmentMonitoring.Database.Data;
using environmentMonitoring.Services;

namespace environmentMonitoring.ViewModels;



public partial class ManageRolesViewModel : IQueryAttributable
{

    

    public ObservableCollection<ViewModels.RoleViewModel> roleList { get; }
    public ICommand NewCommand { get; }
    public ICommand SelectRoleCommand { get; }
    private readonly RolePermissionService _rpService;

    public ManageRolesViewModel(RolePermissionService rolePermissionService)
    {
        _rpService = rolePermissionService;
        
        roleList = new ObservableCollection<ViewModels.RoleViewModel>(_rpService.GetRoleList().Select(r => new RoleViewModel(_rpService, r)));
        
        NewCommand = new AsyncRelayCommand(NewRoleAsync);
        SelectRoleCommand = new AsyncRelayCommand<ViewModels.RoleViewModel>(SelectRoleAsync);
    }

    private async Task NewRoleAsync()
    {
        await Shell.Current.GoToAsync(nameof(Views.RolePage));
    }
       

     private async Task SelectRoleAsync(ViewModels.RoleViewModel role)
    {
        if (role != null) {
            await Shell.Current.GoToAsync($"{nameof(Views.RolePage)}?load={role.role_Id}");
        }
    }




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
                    Shell.Current.GoToAsync($"..");
                }
            }


        }
    }

}
