using System;
namespace voltaire.Models
{
    public class NoteModel : BaseModel
    {
        public NoteModel(Note note)
        {
            Index = note.id +".";
            Name = note.Publisher;
            Date = note.Date.Date.ToString("d");
            Notes = note.Text;
            Image = note.IsReminderActive ? "reminderActive.png" : "reminderInactive.png";
        }

        string index;
        public string Index 
        { 
            get { return index; }
            set
            {
                index = value;
                OnPropertyChanged();
            }
        }

        string name;
        public string Name 
        { 
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        string date;
        public string Date 
        { 
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged();
            }
        }

        string notes;
        public string Notes 
        { 
            get { return notes; }
            set
            {
                notes = value;
                OnPropertyChanged();
            }
        }

        string image;
        public string Image 
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged();
            }
        }
    }
}
