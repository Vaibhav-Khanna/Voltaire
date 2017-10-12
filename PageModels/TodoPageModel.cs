using System;
using voltaire.PageModels.Base;
using voltaire.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using voltaire.PopUps;
using Rg.Plugins.Popup.Services;

namespace voltaire.PageModels
{
    public class TodoPageModel : BasePageModel
    {
        
      
        ReminderAddPopUpModel reminder_popup_model;


        public Command AddNewReminder => new Command(async() =>
       {
           var Reminder = new Reminder() { ReminderDateTime = DateTime.Now.AddDays(1) };
           
           reminder_popup_model = new ReminderAddPopUpModel(Reminder);
           reminder_popup_model.ReminderModeChanged += Reminder_Popup_Model_ReminderSet;
           var popup = new ReminderAddPopUp() { BindingContext = reminder_popup_model };
           await PopupNavigation.PushAsync(popup);
       
        });



        void Reminder_Popup_Model_ReminderSet()
        {
            reminder_popup_model.ReminderModeChanged -= Reminder_Popup_Model_ReminderSet;

            if(!string.IsNullOrEmpty(reminder_popup_model.ReminderName))
            RemindersItemSource.Add(new ReminderModel(reminder_popup_model.Reminder,this));

            if (reminder_popup_model.IsReminderSet == true)
            {

            }
            else if (reminder_popup_model.IsReminderSet == false)
            {

            }
        }



        ObservableCollection<ReminderModel> remindersItemSource { get; set; }
        public ObservableCollection<ReminderModel> RemindersItemSource 
        { 
            get { return remindersItemSource; }
            set
            {
                remindersItemSource = value;

                RaisePropertyChanged();
            }
        }

        public async void DeleteReminder(ReminderModel item)
        {
            var response = await CoreMethods.DisplayAlert(Resources.AppResources.Delete,Resources.AppResources.DeleteConfirm,Resources.AppResources.Ok,Resources.AppResources.NotNow);

            if(response)
            RemindersItemSource.Remove(item);
        }


        public override void Init(object initData)
        {
            base.Init(initData);

            RemindersItemSource = new ObservableCollection<ReminderModel>();
        }


    }
}
