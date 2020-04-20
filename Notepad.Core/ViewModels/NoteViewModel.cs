using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notepad.Core.ViewModels
{
    public class NoteViewModel : MvxNotifyPropertyChanged
    {
        private string _id;
        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _body;
        public string Body
        {
            get => _body;
            set => SetProperty(ref _body, value);
        }
    }
}
