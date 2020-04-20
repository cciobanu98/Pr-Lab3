using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace Notepad.Core.ViewModels
{
    public class LoginPageViewModel : MvxNavigationViewModel
    {
        private readonly IClient _client;
        private readonly IUserDialogs _userDialogs;
        public LoginPageViewModel(IMvxLogProvider provider, IMvxNavigationService navigationService, IClient client, IUserDialogs userDialogs)
           : base(provider, navigationService)
        {
            LoginViewModel = new LoginViewModel();
            _client = client;
            _userDialogs = userDialogs;
        }

        public override async Task Initialize()
        {
            IsConnected = await _client.CheckConnection();
        }
        private LoginViewModel _loginViewModel;
        public LoginViewModel LoginViewModel
        {
            get => _loginViewModel;
            set => SetProperty(ref _loginViewModel, value);
        }

        private IMvxCommand _loginCommand;
        public IMvxCommand LoginCommand => _loginCommand ?? (_loginCommand = new MvxCommand(Login));

        private bool _isConnected;
        public bool IsConnected
        {
            get => _isConnected;
            set => SetProperty(ref _isConnected, value);
        }

        public bool IsNotConnected
        {
            get => !_isConnected;
            set => SetProperty(ref _isConnected, !value);
        }

        private async  void Login()
        {
            var result = await _client.Login(_loginViewModel);
            if (result == true)
            {
                await NavigationService.Navigate<NotesPageViewModel>();
            }
            else
            {
                _userDialogs.Alert("Login failed", "Error");
            }
        }
    }
}
