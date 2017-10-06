using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace voltaire.PopUps
{
	public partial class UserInfoPopUp : PopupPage
	{
		public UserInfoPopUp()
		{
			InitializeComponent();
			Padding = new Thickness(150, 150, 150, 250);
			CloseWhenBackgroundIsClicked = false;
		}
	}
}
