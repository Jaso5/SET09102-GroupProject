using System;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using environmentMonitoring.Database.Data;
using environmentMonitoring.Database.Models;
using environmentMonitoring.Views;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using environmentMonitoring.Services;

namespace environmentMonitoring.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private User? _user;
    private readonly IDiaglogService _diaglogService;
    private readonly IValidationService _validationService;
    private readonly IUserSessionService _userSessionService;


    [ObservableProperty]
    public string? email;
    
    [ObservableProperty]
    public string? password;
    

    public LoginViewModel(IDiaglogService diaglogService, IValidationService validationService, IUserSessionService userSessionService)
    {
        _userSessionService = userSessionService;
        _validationService = validationService;
        _diaglogService = diaglogService;
    }

    [RelayCommand]
    private async Task Login()
    {
        if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
        {
            await _diaglogService.ShowAlertAsync("Fields Cannot be empty", "Please enter username and password", "OK");
            return;
        }

        try {
            _user = await _validationService.CredentialsCheck(Email, Password);
        
            if (_user == null)
            {
                await _diaglogService.ShowAlertAsync("User not found", "Please check your credentials", "OK");
                return;
            }

            await SecureStorage.SetAsync("userId", _user.user_Id.ToString());
            await SecureStorage.SetAsync("userRoleId", _user.role_Id.ToString());
            await SecureStorage.SetAsync("userRole", _user.Role.role_type.ToString());

   
                
                
                await Shell.Current.GoToAsync("//HomePage");  
            
        } catch (Exception) {
            await _diaglogService.ShowAlertAsync("Error", "Login error, please try again", "OK");
        }
    }




}
