using System;
using voltaire.Models;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;

namespace voltaire.PopUps
{
    public class ReminderAddPopUpModel : BaseModel
    {
        public ReminderAddPopUpModel()
        {
        }

        public Command Close => new Command( async() =>
       { 
            await PopupNavigation.PopAsync();
       });


        string remindername;
        public string ReminderName 
        {
            get { return remindername; }
            set
            {
                remindername = value;

                OnPropertyChanged();
            }
        }

    }
}
