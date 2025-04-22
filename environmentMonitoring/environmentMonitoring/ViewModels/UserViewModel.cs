using System;
using environmentMonitoring.Services;
using environmentMonitoring.Database.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace environmentMonitoring.ViewModels;

public class UserViewModel
{
    //private readonly RolePermissionService _rpService;
    private readonly UserService _userService;
    private User _user;

    public int userId => _user.user_Id;
    public string username => _user.first_name + " " + _user.surname;
    public string email => _user.email;

    public int roleId => _user.role_Id;
    public string roleType => _user.Role.role_type;

    public UserViewModel(RolePermissionService rolePermissionService) 
    {
        //_rpService = rolePermissionService;
    }

    public UserViewModel(UserService userService, User user) 
    {
        _userService = userService;
        _user = user;
    }

}
