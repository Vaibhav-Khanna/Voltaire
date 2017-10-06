using System;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using voltaire.PageModels.Base;
using voltaire.PopUps;
using Xamarin.Forms;

namespace voltaire.PageModels
{
	public class HomePageModel : BasePageModel
	{
		public HomePageModel()
		{
		}

		public ICommand UserInfoCommand
		{
			get
			{
				return new Command(async (sender) =>
				{
					UserInfoPopupModel popup_context = new UserInfoPopupModel();
					await PopupNavigation.PushAsync(new UserInfoPopUp() { BindingContext = popup_context }, true);
				});
			}
		}
	}
}
