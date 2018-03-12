using System;
using FreshMvvm;
using Rg.Plugins.Popup.Services;
using voltaire.DataStore.Abstraction;
using voltaire.DataStore.Implementation;
using voltaire.Models;
using voltaire.Models.DataObjects;
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
            LoadData();

        }

        private User _currUser;

        string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                RaisePropertyChanged();
            }
        }
        string phone;
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                RaisePropertyChanged();
            }
        }

        string address;
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                RaisePropertyChanged();
            }
        }

        string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                RaisePropertyChanged();
            }
        }

        string website;
        public string Website
        {
            get { return website; }
            set
            {
                website = value;
                RaisePropertyChanged();
            }
        }

        public Command CloseCommand => new Command(async () =>
       {
           await PopupNavigation.PopAsync(true);
       });


        public Command SignOut => new Command(async () =>
       {

           IsBusy = false;

           await storeManager.SyncAllAsync(true);

           await storeManager.DropEverythingAsync();

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

        private async void LoadData()
        {

            _currUser = await storeManager.UserStore.GetCurrentUserAsync();


            if (_currUser != null)
            {
                UserName = !string.IsNullOrWhiteSpace(_currUser.Name) ? _currUser.Name : Resources.AppResources.NotSpecified;
                Phone = !string.IsNullOrWhiteSpace(_currUser.Phone) ? _currUser.Phone : Resources.AppResources.NotSpecified;
                Address = !string.IsNullOrWhiteSpace(_currUser.ContactAddress) ? _currUser.ContactAddress : Resources.AppResources.NotSpecified;
                Email = !string.IsNullOrWhiteSpace(_currUser.Email) ? _currUser.Email : Resources.AppResources.NotSpecified;
                Website = !string.IsNullOrWhiteSpace(_currUser.Website) ? _currUser.Website : Resources.AppResources.NotSpecified;
            }
        }

    }
}
