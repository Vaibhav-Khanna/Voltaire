using System;
using System.Collections.ObjectModel;
using Rg.Plugins.Popup.Services;
using voltaire.Models;
using voltaire.PageModels.Base;
using Xamarin.Forms;
using System.Linq;
using Xamarin.Forms.GoogleMaps;

namespace voltaire.PopUps
{
    public class AddCustomerPopUpModel : BasePageModel
    {

        public Partner SelectedItem { get; set; }

        ObservableCollection<Partner> partners;
        public ObservableCollection<Partner> Partners { get { return partners; } set { partners = value; RaisePropertyChanged(); } }

        string search;
        public string SearchQuery { get { return search; } set { search = value; Search(); RaisePropertyChanged(); } }


        public delegate void EventHandler();

        public event EventHandler ItemSelectedChanged; //  event handler when a item is selected
       
        async void Search()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                Partners = new ObservableCollection<Partner>();
                return;
            }

            var result = await StoreManager.CustomerStore.Search(SearchQuery.Trim(),null, null);

            if(result!=null && result.Any())
            {
                try
                {
                    Partners = new ObservableCollection<Partner>(result);
                }
                catch(Exception)
                {
                    Partners = new ObservableCollection<Partner>();
                }
            }
        }

        public Command Done => new Command(() =>
        {        
            ItemSelectedChanged.Invoke();
            PopupNavigation.PopAsync(true);
        });


        public Command Close => new Command(() =>
       {
           SelectedItem = null;
           ItemSelectedChanged.Invoke();
           PopupNavigation.PopAsync(true);
       });

    }
}
