using System;
using CommunityToolkit.Mvvm.Input;
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
    private ObservableCollection<UserViewModel> _userList { get; }
    public ICommand SelectUserCommand { get; }
    public ICommand BackCommand { get; }


    public ListUsersForRoleAssignmentViewModel(UserService userService)
    {
        _uService = userService;
        _userList = new ObservableCollection<UserViewModel>(_uService.GetUserList().Select(u => new UserViewModel(_uService, u)));
        SelectUserCommand = new AsyncRelayCommand<UserViewModel>(SelectUserAsync);
        BackCommand = new AsyncRelayCommand(BackAsync);
    }

    private async Task SelectUserAsync(UserViewModel user)
    {
        try {
            await Shell.Current.GoToAsync($"{nameof(Views.AssignRolePage)}?userId={user.userId}");
        } catch (Exception) {
            await Shell.Current.DisplayAlert("Error", "Navigation Error.", "OK");
        }

    }

    private async Task BackAsync()
    {
        try {
            await Shell.Current.GoToAsync(nameof(Views.AdminPanelPage));
        } catch (Exception) {
            await Shell.Current.DisplayAlert("Error", "Navigation Error.", "OK");
        }
    }

}
