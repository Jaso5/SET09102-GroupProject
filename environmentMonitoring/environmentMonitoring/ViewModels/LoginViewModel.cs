using System;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using environmentMonitoring.Database.Data;
using environmentMonitoring.Database.Models;
using environmentMonitoring.Views;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace environmentMonitoring.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private User _user;
    private EnvironmentAppDbContext _context;
    public string email;
    public string password;

    public LoginViewModel(EnvironmentAppDbContext dbContext)
    {
        _context = dbContext;
    }

    

    [RelayCommand]
    private async Task Login()
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            await Application.Current.MainPage.DisplayAlert("Fields Cannot be empty", "Please enter username and password", "OK");
            return;
        }

        _user = await _context.Users.FirstOrDefaultAsync(u => u.email == email && u.password == password);

        if (_user != null)
        {
            // Navigate to the home page
            
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Please check your credentials", "Invalid username or password", "OK");
        }
    }


}
