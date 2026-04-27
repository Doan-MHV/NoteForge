using NoteForge.UI.Models;

namespace NoteForge.UI.Repositories;

public class InMemoryNoteRepository : INoteRepository
{
    private readonly List<Note> _notes = new();


    public IReadOnlyList<Note> GetAll()
    {
        return _notes;
    }

    public void Add(Note note)
    {
        _notes.Add(note);
    }

    public Note? GetById(string id)
    {
        return _notes.FirstOrDefault(note => note.Id == id);
    }
}