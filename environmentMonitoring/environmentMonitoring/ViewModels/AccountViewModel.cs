using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace environmentMonitoring.ViewModels;

	public partial class AccountViewModel
	{
        public ICommand BackCommand { get; }

        public AccountViewModel()
        {

            BackCommand = new AsyncRelayCommand(NavigateToHomePage);
        }

        [RelayCommand]
        private async Task NavigateToHomePage()
        {
            await Shell.Current.GoToAsync("///HomePage");
        }

    }
		
	