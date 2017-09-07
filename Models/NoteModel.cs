using System;
using FreshMvvm;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using voltaire.PopUps;

namespace voltaire.Models
{
    public class NoteModel : BaseModel
    {
        public NoteModel(Note note)
        {
            Note = note;
           
            Index = note.id +".";
            Name = note.Publisher;
            Date = note.Date.Date.ToString("d");
            Notes = note.Text;
            IsReminderactive = note.IsReminderActive;
        }

        public Note Note { get; set; }

        public Command ReminderToggle => new Command( async(obj) =>
       {
            await PopupNavigation.PushAsync(new ReminderAddPopUp());
            IsReminderactive = !IsReminderactive;
       });

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

        bool isreminderactive;
        public bool IsReminderactive
        {
            get { return isreminderactive; }
            set 
            {
                isreminderactive = value;
                Note.IsReminderActive = isreminderactive;

                Image = isreminderactive ? "reminderActive.png" : "reminderInactive.png";

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
