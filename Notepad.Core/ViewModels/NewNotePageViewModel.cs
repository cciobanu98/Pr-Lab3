using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Notepad.Core.ViewModels
{
    public class NewNotePageViewModel : MvxNavigationViewModel
    {
        private readonly IClient _client;
        public NewNotePageViewModel(IMvxLogProvider provider, IMvxNavigationService navigationService, IClient client)
          : base(provider, navigationService)
        {
            _client = client;
            _note = new NoteViewModel();
        }

        private NoteViewModel _note;

        public NoteViewModel Note
        {
            get => _note;
            set => SetProperty(ref _note, value);
        }

        private IMvxCommand _createNoteCommand;

        public IMvxCommand CreateNoteCommand => _createNoteCommand ?? (_createNoteCommand = new MvxCommand(CreateNote));

        private async void CreateNote()
        {
            await _client.CreateNote(_note);
            await NavigationService.Close(this);
        }
    }
}
