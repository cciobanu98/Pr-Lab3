using MvvmCross.Forms.Views;
using Notepad.Core.ViewModels;
using Xamarin.Forms.Xaml;

namespace Notepad.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : MvxContentPage<LoginPageViewModel>
    {
        public LoginPage()
        {
            InitializeComponent();
        }
    }
}