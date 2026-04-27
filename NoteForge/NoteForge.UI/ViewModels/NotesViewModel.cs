using System.Collections.ObjectModel;
using NoteForge.UI.Models;
using NoteForge.UI.Repositories;

namespace NoteForge.UI.ViewModels;

public class NotesViewModel : BaseViewModel
{
    private readonly INoteRepository _repository;

    private string _content = "";

    private string _errorMessage = "";

    private bool _isSaving;

    private Note? _selectedNote;

    private string _title = "";

    public NotesViewModel(INoteRepository repository)
    {
        _repository = repository;
        CreateNoteCommand = new Command(CreateNote);
        SaveNoteCommand = new Command(SaveSelectedNote);
        ClearEditorCommand = new Command(ClearEditor);
    }

    public Command CreateNoteCommand { get; }
    public Command SaveNoteCommand { get; }
    public Command ClearEditorCommand { get; }

    public string Title
    {
        get => _title;
        set
        {
            if (_title == value) return;
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }

    public string Content
    {
        get => _content;
        set
        {
            if (_content == value) return;
            _content = value;
            OnPropertyChanged(nameof(Content));
        }
    }

    public Note? SelectedNote
    {
        get => _selectedNote;
        set
        {
            if (_selectedNote == value) return;
            _selectedNote = value;

            OnPropertyChanged(nameof(SelectedNote));

            if (_selectedNote == null)
            {
                Title = "";
                Content = "";
            }
            else
            {
                Title = _selectedNote.Title;
                Content = _selectedNote.Content;
            }
        }
    }

    public bool IsSaving
    {
        get => _isSaving;
        set
        {
            if (_isSaving == value) return;
            _isSaving = value;
            OnPropertyChanged(nameof(IsSaving));
        }
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set
        {
            if (_errorMessage == value) return;
            _errorMessage = value;
            OnPropertyChanged(nameof(ErrorMessage));
        }
    }

    public ObservableCollection<Note> Notes { get; } = new();


    public void LoadNotes()
    {
        Notes.Clear();
        foreach (var note in _repository.GetAll()) Notes.Add(note);
    }

    public void CreateNote()
    {
        var note = new Note(Guid.NewGuid().ToString(), "Untitled", "");

        _repository.Add(note);
        Notes.Add(note);
        SelectedNote = note;
    }

    public void SaveSelectedNote()
    {
        ErrorMessage = "";

        try
        {
            if (SelectedNote == null) return;
            SelectedNote.UpdateTitle(Title);
            SelectedNote.UpdateContent(Content);
        }
        catch (ArgumentException e)
        {
            ErrorMessage = e.Message;
        }
    }

    public void ClearEditor()
    {
        SelectedNote = null;
        Title = "";
        Content = "";
        ErrorMessage = "";
    }
}