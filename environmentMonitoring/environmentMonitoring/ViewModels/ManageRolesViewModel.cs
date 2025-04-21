using System;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using environmentMonitoring.Database.Data;

namespace environmentMonitoring.ViewModels;



public partial class ManageRolesViewModel : IQueryAttributable
{

    

    public ObservableCollection<ViewModels.RoleViewModel> roleList { get; }
    public ICommand NewCommand { get; }
    public ICommand SelectRoleCommand { get; }

    private readonly EnvironmentAppDbContext _context;

    public ManageRolesViewModel(EnvironmentAppDbContext context)
    {
        _context = context;

        // unhandled sql exception when database isn't open - connection
        roleList = new ObservableCollection<ViewModels.RoleViewModel>(_context.Roles.ToList().Select(r => new RoleViewModel(_context, r)));
        
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

        
            // If note exists, delete it
            if (matchedRole != null)
                roleList.Remove(matchedRole);
        }
        else if (query.ContainsKey("saved"))
        {
            string roleId = query["saved"].ToString();
            RoleViewModel matchedRole = roleList.Where((r) => r.role_Id == int.Parse(roleId)).FirstOrDefault();

        
            // If note is found, update it
            if (matchedRole != null)
            {
                matchedRole.Reload();
                roleList.Move(roleList.IndexOf(matchedRole), 0);
            }
        
            // If note isn't found, it's new; add it.
            else
                 roleList.Insert(0, new RoleViewModel(_context, _context.Roles.Single(r => r.role_Id == int.Parse(roleId))));


        }
    }

}
