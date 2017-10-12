using System;
using System.Collections.ObjectModel;
using Rg.Plugins.Popup.Services;
using voltaire.PopUps;
using Xamarin.Forms;
using voltaire.PageModels;

namespace voltaire.Models
{
    public class ReminderModel : BaseModel
    {
        
        public ReminderModel(Reminder reminder,Object parentContext)
        {
            Reminder = reminder;
            ParentContext = parentContext;
        }

        object ParentContext;

        Reminder _reminder;
        public Reminder Reminder
        {
            get { return _reminder; }
            set
            {
                _reminder = value;

                Title = _reminder.Name;
                Date = _reminder.ReminderDateTime;
                Priority = _reminder.Priority;

                if (DateTime.Now.CompareTo(Date) >= 0)
                    IsLate = true;
                else
                    IsLate = false;

                RaisePropertyChanged();
            }
        }

        private ReminderAddPopUpModel reminder_popup_model;

        string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                RaisePropertyChanged();
            }
        }

        DateTime date;
        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                RaisePropertyChanged();
            }
        }

        ReminderPriority priority;
        public ReminderPriority Priority
        {
            get { return priority; }
            set
            {
                priority = value;
                RaisePropertyChanged();
            }
        }

        bool islate;
        public bool IsLate
        {
            get { return islate; }
            set
            {
                islate = value;
                RaisePropertyChanged();
            }
        }


        public Command Modify => new Command(async() =>
       {
           reminder_popup_model = new ReminderAddPopUpModel(Reminder);
           reminder_popup_model.ReminderModeChanged += Reminder_Popup_Model_ReminderSet;
           var popup = new ReminderAddPopUp() { BindingContext = reminder_popup_model };
           await PopupNavigation.PushAsync(popup);
       });

        public Command Delete => new Command( () =>
       {
            if(ParentContext!=null)
            {
                var context = ParentContext as TodoPageModel;
               
                if(context!=null)
                {
                    context.DeleteReminder(this);
                }

            }
       });


        void Reminder_Popup_Model_ReminderSet()
        {
            reminder_popup_model.ReminderModeChanged -= Reminder_Popup_Model_ReminderSet;

            Reminder = reminder_popup_model.Reminder;

            if (reminder_popup_model.IsReminderSet == true)
            {
                
            }
            else if (reminder_popup_model.IsReminderSet == false)
            {
                
            }
        }

    }
}
