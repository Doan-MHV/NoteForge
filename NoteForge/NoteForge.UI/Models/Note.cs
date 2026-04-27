using System.ComponentModel;

namespace NoteForge.UI.Models;

public class Note : INotifyPropertyChanged
{
    public Note(string id, string title, string content)
    {
        Id = id;

        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title cannot be null or empty.");

        Title = title.Trim();
        Content = content;

        var now = DateTime.UtcNow;
        CreatedAt = now;
        UpdatedAt = now;
    }

    public string Id { get; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; private set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void UpdateContent(string content)
    {
        Content = content;
        UpdatedAt = DateTime.UtcNow;
        OnPropertyChanged(nameof(Content));
        OnPropertyChanged(nameof(UpdatedAt));
    }

    public void UpdateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title cannot be null or empty.");

        Title = title.Trim();
        UpdatedAt = DateTime.UtcNow;
        OnPropertyChanged(nameof(Title));
        OnPropertyChanged(nameof(UpdatedAt));
    }
}