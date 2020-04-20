using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Notepad.Core.ViewModels
{
    public class NotesPageViewModel : MvxNavigationViewModel
    {
        private readonly IClient _client;
        public NotesPageViewModel(IMvxLogProvider provider, IMvxNavigationService navigationService, IClient client)
          : base(provider, navigationService)
        {
            Notes = new MvxObservableCollection<NoteViewModel>();
            _client = client;
            
        }

        private MvxObservableCollection<NoteViewModel> _notes;
        public MvxObservableCollection<NoteViewModel> Notes
        {
            get => _notes;
            set => SetProperty(ref _notes, value);
        }
        public override void ViewAppearing()
        {
            new Thread(LoadNotes).Start();
            base.ViewAppearing();
        }
        private IMvxCommand _goToCreateNoteCommand;
        public IMvxCommand GoToCreateNoteCommand => _goToCreateNoteCommand ?? (_goToCreateNoteCommand = new MvxCommand(GoToCreateNote));

        private IMvxCommand<NoteViewModel> _goToEditNoteCommand;
        public IMvxCommand<NoteViewModel> GoToEditNoteCommand => _goToEditNoteCommand ?? (_goToEditNoteCommand = new MvxCommand<NoteViewModel>(GoToEdit));

        private IMvxCommand<NoteViewModel> _deleteNoteCommand;
        public IMvxCommand<NoteViewModel> DeleteNoteCommand => _deleteNoteCommand ?? (_deleteNoteCommand = new MvxCommand<NoteViewModel>(DeleteNote));

        private async void DeleteNote(NoteViewModel note)
        {
            await _client.DeleteNote(note.Id);
            var notes = await _client.GetNotes();
            Notes = new MvxObservableCollection<NoteViewModel>(notes);
        }

        private async void LoadNotes()
        {
            var notes = await _client.GetNotes();
            Notes = new MvxObservableCollection<NoteViewModel>(notes);
        }
        private void GoToEdit(NoteViewModel noteViewModel)
        {
            NavigationService.Navigate<EditNotePageViewModel, NoteViewModel>(noteViewModel);
        }

        private void GoToCreateNote()
        {
            NavigationService.Navigate<NewNotePageViewModel>();
        }
    }
}
