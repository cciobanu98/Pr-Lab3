using Notepad.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notepad.Core
{
    public interface IClient
    {
        Task<bool> CheckConnection();

        Task<bool> Login(LoginViewModel login);

        Task<IEnumerable<NoteViewModel>> GetNotes();

        Task<NoteViewModel> GetNote(string id);

        Task CreateNote(NoteViewModel note);

        Task DeleteNote(string id);

        Task EditNote(NoteViewModel note);
    }
}
