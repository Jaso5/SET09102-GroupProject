using System;
using environmentMonitoring.Database.Models;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using environmentMonitoring.Database.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using environmentMonitoring.Services;

namespace environmentMonitoring.ViewModels;

public partial class RoleViewModel: ObservableObject, IQueryAttributable
{
    private environmentMonitoring.Database.Models.Role _role;
    private readonly EnvironmentAppDbContext _context;

    public string role_type
    {
        get => _role.role_type;
        set
        {
            if (_role.role_type != value)
            {
                _role.role_type = value;
                OnPropertyChanged();
            }
        }
    }

    public int role_Id => _role.role_Id;
    

    public RoleViewModel(EnvironmentAppDbContext context)
    {
        _context = context;
        _role = new Role();
    }

    public RoleViewModel(EnvironmentAppDbContext context, Role role)
    {
        _context = context;
        _role = role;
    }



    [RelayCommand]
    private async Task Save()
    {
        if (string.IsNullOrWhiteSpace(role_type))
        {
            await Shell.Current.DisplayAlert("Field Empty", "Pleaser enter a role name.", "OK");
            return;
        }
        else if(_role.role_Id == 0 && RoleExists() || _role.role_Id != 0 && RoleExists())
        {
            await Shell.Current.DisplayAlert("Role Exists", "Please enter a different role name.", "OK");
            return;
        }
        else
        {
            bool create = await Shell.Current.DisplayAlert("Confirm", "Are you sure you want to create this role?", "Yes", "No");
            if (!create) return;
                try {
                    if (_role.role_Id == 0)
                    {
                        _context.Roles.Add(_role);
                    }
                    else if (_role.role_Id != 0)
                    {
                        _context.Roles.Update(_role); 
                    }
                    _context.SaveChanges();
                    await Shell.Current.GoToAsync($"..?saved={_role.role_Id}");
                } catch (Exception)
                {
                    await Shell.Current.DisplayAlert("Error", "Roles cannot be created or updated at this time", "OK");
                }
        }
    }

    [RelayCommand]
    private async Task Delete()
    {
        if (role_Id != 0) {
            bool confirmation = await Shell.Current.DisplayAlert("Confirm", "Are you sure you want to delete this role?", "Yes", "No");
            if (!confirmation) return;
                try {
                    _context.Remove(_role);
                    _context.SaveChanges();
                    await Shell.Current.GoToAsync($"..?deleted={_role.role_Id}");
                } catch (Exception) {
                    await Shell.Current.DisplayAlert("Error", "An error occurred while deleting the role.", "OK");
                }
        }
        else {
            await Shell.Current.DisplayAlert("Error", "can't delete an empty role", "OK");
        }
    }




    private bool RoleExists()
    {
        return _context.Roles.Any(r => r.role_type == role_type && r.role_Id != _role.role_Id);
    }
      


    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("load"))
        {
            _role = _context.Roles.Single(r => r.role_Id == int.Parse(query["load"].ToString()));
        }
        else
        {
            _role = new Role();
        }
        RefreshProperties();
    }

     public void Reload()
    {
        _context.Entry(_role).Reload();
        RefreshProperties();
    }

     private void RefreshProperties()
    {
        OnPropertyChanged(nameof(role_type));
    }

    [RelayCommand]
    private async Task NavigateToPermissions()
    {
        if (_role.role_Id == 0)
        {
            await Shell.Current.DisplayAlert("Role hasn't been created", "Please save the role before managing permissions.", "OK");
            return;
        }

        await Shell.Current.GoToAsync($"{nameof(Views.ManageRolePermissionsPage)}?load={_role.role_Id}");
        
    }

}
