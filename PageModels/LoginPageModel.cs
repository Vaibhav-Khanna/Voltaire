using System;
using voltaire.PageModels.Base;
using Xamarin.Forms;
using voltaire.Resources;
using FreshMvvm;
using voltaire.DataStore.Implementation;
using voltaire.DataStore.Abstraction;

namespace voltaire.PageModels
{
    public class LoginPageModel : BasePageModel
    {

        private StoreManager storeManager = DependencyService.Get<IStoreManager>() as StoreManager;

       
        string username;
        public string UserName 
        {
            get { return username; }
            set
            {
                username = value;
                RaisePropertyChanged();
            }
        }

        string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                RaisePropertyChanged();
            }
        }

        public Command Login => new Command(async(obj) =>
       {
            
            if(string.IsNullOrEmpty(UserName)) 
            {
                await CoreMethods.DisplayAlert(AppResources.InformationMissing,AppResources.UsernameMissing,AppResources.Ok);
                return;
            }

            if(string.IsNullOrEmpty(Password))
            {
                await CoreMethods.DisplayAlert(AppResources.InformationMissing, AppResources.PasswordMissing, AppResources.Ok);
                return;
            }

            IsBusy = true;

            var response = await storeManager.LoginAsync(UserName,Password);

            IsBusy = false;

            if(response==null || string.IsNullOrEmpty(response.MobileServiceAuthenticationToken))
            {
                await CoreMethods.DisplayAlert(AppResources.Error, AppResources.IncorrectInfo, AppResources.Ok);
            }
            else
            {
                Device.BeginInvokeOnMainThread( () => 
                {
                    var homePage = FreshPageModelResolver.ResolvePageModel<HomePageModel>();

                    var homeContainer = new FreshNavigationContainer(homePage) { BarBackgroundColor = (Color)App.Current.Resources["turquoiseBlue"], BarTextColor = Color.White };

                    App.Current.MainPage = homeContainer;

                });    
            }

       });

    }
}
