using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace voltaire.PopUps
{
    public partial class ReminderAddPopUp : PopupPage
    {
        public ReminderAddPopUp()
        {
            InitializeComponent();

            Padding = new Thickness(150, 90, 150, 200);

            CloseWhenBackgroundIsClicked = false;
		}

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            
            if(string.IsNullOrWhiteSpace(remindername.Text))
            {
                await DisplayAlert("Fill in the name","Please enter the name for this reminder.","Ok");
            }
            else
            {
                var context =  BindingContext as ReminderAddPopUpModel;
                if (context == null)
                    return;
                
                context.Done.Execute(null);
            }

        }

        protected override bool OnBackButtonPressed()
        {
            (BindingContext as ReminderAddPopUpModel).Close.Execute(null);
            return true;
        }
    }
}
