using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using voltaire.Resources;

namespace voltaire.PopUps
{
	public partial class UserInfoPopUp : PopupPage
	{
		public UserInfoPopUp()
		{
			InitializeComponent();
			
			CloseWhenBackgroundIsClicked = false;
		}

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            var response = await DisplayAlert(AppResources.LogOut,AppResources.LogOutMessage,AppResources.Ok,AppResources.Cancel);

            if(response)
            {
                (this.BindingContext as UserInfoPopupModel).SignOut.Execute(null);
            }
        }

        protected override bool OnBackButtonPressed()
        {
            (BindingContext as UserInfoPopupModel).CloseCommand.Execute(null);
            return true;
        }
    }
}
