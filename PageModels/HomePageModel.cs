using System;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using voltaire.Models.DataObjects;
using voltaire.PageModels.Base;
using voltaire.PopUps;
using Xamarin.Forms;

namespace voltaire.PageModels
{
    public class HomePageModel : BasePageModel
    {
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
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            _currUser = await StoreManager.UserStore.GetCurrentUserAsync();

            UserName = _currUser != null ? _currUser.Name : string.Empty;
        }



    }
}
