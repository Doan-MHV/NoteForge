using NoteForge.UI.Models;

namespace NoteForge.UI.Repositories;

public interface INoteRepository
{
    IReadOnlyList<Note> GetAll();
    void Add(Note note);
    Note? GetById(string id);
}