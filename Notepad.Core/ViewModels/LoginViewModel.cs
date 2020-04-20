using MvvmCross.ViewModels;

namespace Notepad.Core.ViewModels
{
    public class LoginViewModel : MvxNotifyPropertyChanged
    {
        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
    }
}
