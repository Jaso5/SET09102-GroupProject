using System;
using environmentMonitoring.Database.Models;
using environmentMonitoring.ViewModels;
using environmentMonitoring.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Diagnostics;

namespace environmentMonitoring.ViewModels;

public class ListUsersForRoleAssignmentViewModel
{

    private readonly UserService _uService;
    public ObservableCollection<UserViewModel> userList { get; }
    //public ICommand SelectRoleCommand { get; }

    public ListUsersForRoleAssignmentViewModel(UserService userService)
    {
        _uService = userService;
        userList = new ObservableCollection<UserViewModel>(_uService.GetUserList().Select(u => new UserViewModel(_uService, u)));
        //SelectRoleCommand = new AsyncRelayCommand<UserViewModel>(SelectUserAsync);
        Debug.WriteLine(userList.Count);
    }

    private async Task SelectUserAsync(UserViewModel user)
    {
        try {
            await Shell.Current.GoToAsync(nameof(Views.AssignRolePage));
            {
                
            }
        } catch (Exception) {
            await Shell.Current.DisplayAlert("Error", "Navigation Error.", "OK");
        }

    }

}
