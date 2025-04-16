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
            await Application.Current.MainPage.DisplayAlert("Fields Cannot be empty", "Please enter username and password", "OK");
            return;
        }

        await _context.Users
        .Include(u => u.first_name)
        .Include(u => u.surname)
        .Include(u => u.Role) // Include the Role navigation property
        .FirstOrDefaultAsync(u => u.email == Email && u.password == Password);

        if (_user != null)
        {
            // Navigate to the home page
            await SecureStorage.SetAsync("hasAuth", "true");
            await SecureStorage.SetAsync("userId", _user.user_Id.ToString());
            await SecureStorage.SetAsync("userRole", _user.Role.role_type);
            await SecureStorage.SetAsync("userName", _user.first_name + " " + _user.surname);
            
            await Shell.Current.GoToAsync("//HomePage");

            
        }
        else
        {
            await _diaglogService.ShowAlertAsync("Please check your credentials", "Invalid username or password", "OK");
        }
    }




}
