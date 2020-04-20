using MvvmCross.Forms.Views;
using Notepad.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notepad.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditNotePage : MvxContentPage<EditNotePageViewModel>
    {
        public EditNotePage()
        {
            InitializeComponent();
        }
    }
}