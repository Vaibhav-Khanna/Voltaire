using System;
using Rg.Plugins.Popup.Services;
using voltaire.Models;
using Xamarin.Forms;

namespace voltaire
{
	public class UserInfoPopupModel : BaseModel
	{
		public UserInfoPopupModel()
		{
		}

		public Command CloseCommand => new Command(() =>
		{
			PopupNavigation.PopAsync(true);
		});
	}
}
