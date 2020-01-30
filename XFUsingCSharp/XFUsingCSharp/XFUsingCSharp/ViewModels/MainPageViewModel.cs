using XFUsingCSharp.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace XFUsingCSharp.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public MainPageViewModel()
        {
            Notes = new ObservableCollection<Note>();

            SaveNoteCommand = new Command(() =>
            {
                if (!string.IsNullOrEmpty(NoteText))
                {
                    Notes.Add(new Note { Text = NoteText });
                    NoteText = string.Empty; 
                }
            });
            
            DeleteNotesCommand = new Command(() => Notes.Clear());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        string noteText;
        public string NoteText
        {
            get => noteText;
            set
            {
                noteText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NoteText)));

                SaveNoteCommand.ChangeCanExecute();
            }
        }

        public ObservableCollection<Note> Notes { get; }
        public Command SaveNoteCommand { get; }
        public Command DeleteNotesCommand { get; }
    }
}
