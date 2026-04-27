using NoteForge.UI.Repositories;
using NoteForge.UI.ViewModels;

namespace NoteForge.UI;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        var vm = new NotesViewModel(new InMemoryNoteRepository());
        BindingContext = vm;
        vm.LoadNotes();
    }
}