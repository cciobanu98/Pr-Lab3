using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Notepad.Core.ViewModels
{
    public class EditNotePageViewModel : MvxNavigationViewModel<NoteViewModel>
    {
        private readonly IClient _client;
        public EditNotePageViewModel(IMvxLogProvider provider, IMvxNavigationService navigationService, IClient client)
          : base(provider, navigationService)
        {
            _client = client;
        }

        private NoteViewModel _note;

        public NoteViewModel Note
        {
            get => _note;
            set => SetProperty(ref _note, value);
        }

        public override async void Prepare(NoteViewModel parameter)
        {
            var note = await _client.GetNote(parameter.Id);
            Note = note;
        }

        private IMvxCommand _saveNoteCommand;

        public IMvxCommand SaveNoteCommand => _saveNoteCommand ?? (_saveNoteCommand = new MvxCommand(SaveNote));

        private async  void SaveNote()
        {
           await  _client.EditNote(Note);
            await NavigationService.Close(this);
        }
    }
}
