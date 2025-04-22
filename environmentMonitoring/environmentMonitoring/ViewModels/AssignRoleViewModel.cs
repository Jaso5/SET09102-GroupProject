using System;
using CommunityToolkit.Mvvm.Input;
using environmentMonitoring.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using environmentMonitoring.Database.Models;

namespace environmentMonitoring.ViewModels;

public class AssignRoleViewModel: IQueryAttributable
{
    public ObservableCollection<RoleViewModel> roleList { get; }
    private readonly RolePermissionService _rpService;
    private readonly UserService _userService;

    public ICommand BackCommand { get; }

    public ICommand SelectRoleCommand { get; }

    public User _user;


    public AssignRoleViewModel(RolePermissionService rolePermissionService, UserService userService) {

        _rpService = rolePermissionService;
        _userService = userService;
        roleList = new ObservableCollection<RoleViewModel>(_rpService.GetRoleList().Select(r => new RoleViewModel(r)));
        BackCommand = new AsyncRelayCommand(BackAsync);
        SelectRoleCommand = new AsyncRelayCommand<RoleViewModel>(SelectRoleAsync);

    }

    private async Task BackAsync()
    {
        try {
            await Shell.Current.GoToAsync(nameof(Views.ListUsersForRoleAssignmentPage));
        } catch (Exception) {
            await Shell.Current.DisplayAlert("Error", "Navigation Error.", "OK");
        }
    }

    private async Task SelectRoleAsync(RoleViewModel role)
    {
        bool confirmation = await Shell.Current.DisplayAlert("Confirm", "Are you sure you want to update users role?", "Yes", "No");
            if (role != null && confirmation) {
                try {
                    _user.Role = role._role;
                    _userService.UpdateUserRole(_user);
                    await Shell.Current.GoToAsync("///ListUsersForRoleAssignmentPage");
                    
                } catch (Exception) {
                    await Shell.Current.DisplayAlert("Error", "Error when updating users role.", "OK");
                }
            }
    }





    /*! IQueryAttributable.ApplyQueryAttributes method 
     *  
     *  
     *  @param A query dictionary containing the usersId 
     *  Displays error message if there is an issue when attempting to retrieve the role
     */ 
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("userId"))
        {
            try {
            _user = _userService.GetUserById(int.Parse(query["userId"].ToString()));
            } catch (Exception) {
                Shell.Current.DisplayAlert("Error", "Unable to retrieve the user", "OK");
                return;
            }
        }
    }

}
