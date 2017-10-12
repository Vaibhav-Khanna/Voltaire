using System;
using voltaire.Models;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
using FreshMvvm;

namespace voltaire.PopUps
{
    public class ReminderAddPopUpModel : BaseModel
    {
        public ReminderAddPopUpModel(Reminder _reminder )
        {
            Reminder = _reminder;
        }

		public event EventHandler ReminderModeChanged;

		public delegate void EventHandler();

        Reminder reminder;
        public Reminder Reminder
        {
            get { return reminder; }
            set
            {
                reminder = value;

                ReminderName = reminder.Name;

                ReminderDate = reminder.ReminderDateTime.Date;

                Time = reminder.ReminderDateTime.TimeOfDay;

                SelectedItem = reminder.Priority.ToString();

                RaisePropertyChanged();
            }
        }

        public bool? IsReminderSet;


        public Command Close => new Command( async() =>
       { 
            await PopupNavigation.PopAsync();
          
            IsReminderSet = null;
            ReminderModeChanged.Invoke();
       });

        public Command Cancel => new Command(async () =>
       {
		    await PopupNavigation.PopAsync();
		 
            IsReminderSet = false;
		  
            ReminderModeChanged.Invoke();
       });


      public Command Done => new Command(async() =>
      {
            reminder.Name = ReminderName;
            reminder.ReminderDateTime = ReminderDate.Date + time;
            reminder.ReminderDateTime = reminder.ReminderDateTime.Date + Time; 
            reminder.Priority = ParseEnum<ReminderPriority>(SelectedItem);

            await PopupNavigation.PopAsync(); 
            IsReminderSet = true;
            ReminderModeChanged.Invoke();
      });

        string remindername;
        public string ReminderName 
        {
            get { return remindername; }
            set
            {
                remindername = value;

                RaisePropertyChanged();
            }
        }

        DateTime reminderdate;
        public DateTime ReminderDate
        {
            get { return reminderdate; }
            set 
            {
                reminderdate = value;
                RaisePropertyChanged();
            }
        }

        TimeSpan time;
        public TimeSpan Time
        {
            get { return time; }
            set
            {

                time = value;

                RaisePropertyChanged();
            }
        }

        string selecteditem;
        public string SelectedItem
        {
            get { return selecteditem; }
            set
            {
                selecteditem = value;

                RaisePropertyChanged();
            }
        }

        public List<string> Source { get; set; } = new List<string>() { "None", "Low", "Medium", "High" };

		public static T ParseEnum<T>(string value)
		{
			return (T)Enum.Parse(typeof(T), value, true);
		}

    }
}
