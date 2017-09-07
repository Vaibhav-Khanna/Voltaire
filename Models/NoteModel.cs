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

        private ReminderAddPopUpModel reminder_popup_model;

        public Command ReminderToggle => new Command( async(obj) =>
       {            
                reminder_popup_model = new ReminderAddPopUpModel(Note.Reminder);
                reminder_popup_model.ReminderModeChanged += Reminder_Popup_Model_ReminderSet;
                var popup = new ReminderAddPopUp(){ BindingContext = reminder_popup_model };
                await PopupNavigation.PushAsync(popup);
       });


        void Reminder_Popup_Model_ReminderSet()
        {
            reminder_popup_model.ReminderModeChanged -= Reminder_Popup_Model_ReminderSet;

            if (reminder_popup_model.IsReminderSet == true)
                IsReminderactive = true;
            else if (reminder_popup_model.IsReminderSet == false)
                IsReminderactive = false;
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
