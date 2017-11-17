using System;
using FreshMvvm;
using Rg.Plugins.Popup.Services;
using voltaire.DataStore.Abstraction;
using voltaire.DataStore.Implementation;
using voltaire.Models;
using voltaire.PageModels;
using Xamarin.Forms;

namespace voltaire
{
	public class UserInfoPopupModel : BaseModel
	{

        private StoreManager storeManager;


		public UserInfoPopupModel()
		{
            storeManager = DependencyService.Get<IStoreManager>() as StoreManager;
		}

        public Command CloseCommand => new Command( async() =>
		{
            await PopupNavigation.PopAsync(true);
		});


        public Command SignOut => new Command(async () =>
       {

           IsBusy = false;

           //await storeManager.SyncAllAsync(true);

           var result = await storeManager.LogoutAsync();

           if (!result)
           {
               IsBusy = true; 
               return;
           }

           Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
           {
               var homePage = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();

               App.Current.MainPage = new FreshNavigationContainer(homePage) { BarBackgroundColor = (Color)App.Current.Resources["turquoiseBlue"], BarTextColor = Color.Black };
           });

           await PopupNavigation.PopAsync(true);

           IsBusy = true;

       });

	}
}
