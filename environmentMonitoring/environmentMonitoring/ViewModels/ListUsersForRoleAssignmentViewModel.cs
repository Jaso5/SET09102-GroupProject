using CommunityToolkit.Mvvm.Input;
using environmentMonitoring.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace environmentMonitoring.ViewModels;

/*! ListUsersForRoleAssignmentViewModel is responsible for updating a list of users 
     *  Displays error message if there is an issue when attempting to retrieve the role
     */ 

public class ListUsersForRoleAssignmentViewModel: ObservableObject, IQueryAttributable
{

    private readonly UserService _uService;
    private ObservableCollection<UserViewModel> _userList { get; set;}
    public ObservableCollection<UserViewModel>  userList  {
       get { return  _userList;  }
       set {
            if(_userList != value) {
                _userList = value;
                OnPropertyChanged(nameof(userList));
            }
       }
    }
    public ICommand SelectUserCommand { get; }
    public ICommand BackCommand { get; }

    public ListUsersForRoleAssignmentViewModel()
    {
        
    }

    public ListUsersForRoleAssignmentViewModel(UserService userService)
    {
        _uService = userService;
        _userList = new ObservableCollection<UserViewModel>(_uService.GetUserList().Select(u => new UserViewModel(_uService, u)));
        SelectUserCommand = new AsyncRelayCommand<UserViewModel>(SelectUserAsync);
        BackCommand = new AsyncRelayCommand(BackAsync);
    }

    /*! SelectUserAsync method is used to select a user from the list and navigate
     *  to and pass the users ID over to the role assigning page 
     *  Display and error if there is an issue during navigation
     */ 
    private async Task SelectUserAsync(UserViewModel user)
    {
        try {
            await Shell.Current.GoToAsync($"{nameof(Views.AssignRolePage)}?userId={user.userId}");
        } catch (Exception) {
            await Shell.Current.DisplayAlert("Error", "Navigation Error.", "OK");
        }

    }

    /*! BackAsync method navigates the user back to the admin panel
     *  Display and error if there is an issue during navigation
     */ 
    private async Task BackAsync()
    {
        try {
            await Shell.Current.GoToAsync(nameof(Views.AdminPanelPage));
        } catch (Exception) {
            await Shell.Current.DisplayAlert("Error", "Navigation Error.", "OK");
        }
    }

    /*! updateUserList method updates the userList from the database
     *  Display and error if there is an issue retrieving the updated list
     */ 
    private async Task UpdateUserList() {
        try {
            userList = new ObservableCollection<UserViewModel>(_uService.GetUserList().Select(u => new UserViewModel(_uService, u)));
        } catch(Exception) {
            await Shell.Current.DisplayAlert("Error", "Page couldn't be updated", "OK");
            await Shell.Current.GoToAsync("///HomePage");
        }
    }

    /*! IQueryAttributable.ApplyQueryAttributes method calls the updateUserList method to ensure the UI stays up to date
     *  with current changes
     *  
     *  @param A query dictionary containing the refresh key
     *  Displays error message if there is an issue when attempting to update the list
     */ 
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("refresh"))
        {
            try {
                UpdateUserList();
            } catch (Exception) {
                Shell.Current.DisplayAlert("Error", "There was an error updating the list, please reload the application", "OK");
                return;
            }
        }
    }

}
