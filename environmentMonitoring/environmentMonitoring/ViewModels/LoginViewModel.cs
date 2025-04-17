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
    private User _user;
    private readonly IDiaglogService _diaglogService;
    private EnvironmentAppDbContext _context;

    [ObservableProperty]
    public string email;
    
    [ObservableProperty]
    public string password;
    

    public LoginViewModel(EnvironmentAppDbContext dbContext)
    {
        _context = dbContext;
        _diaglogService = new DiaglogService();
        
    }


    [RelayCommand]
    private async Task Login()
    {
        if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
        {
            await _diaglogService.ShowAlertAsync("Fields Cannot be empty", "Please enter username and password", "OK");
            return;
        }

         var user = await _context.Users
        .Include(u => u.Role)
        .FirstOrDefaultAsync(u => u.email == Email);
        
        if (user == null)
        {
            await _diaglogService.ShowAlertAsync("User not found", "Please check your credentials", "OK");
            return;
        }

        if (user != null && user.password == Password) 
        {
            _user = user;
            // Navigate to the home page
            await SecureStorage.SetAsync("hasAuth", "true");
            await SecureStorage.SetAsync("userId", _user.user_Id.ToString());
            await SecureStorage.SetAsync("userRole", _user.Role.role_type);
            await SecureStorage.SetAsync("userName", _user.first_name + " " + _user.surname);
            
            await Shell.Current.GoToAsync("//HomePage");  
        }
    }




}
