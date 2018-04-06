using System;
using System.Collections.ObjectModel;
using System.Linq;
using Rg.Plugins.Popup.Services;
using voltaire.Models;
using voltaire.PageModels.Base;
using Xamarin.Forms;
using voltaire.Models.DataObjects;
using System.Collections.Generic;

namespace voltaire.PopUps
{
    public class SearchStateCountryPopUpModel : BasePageModel
    {
        public object SelectedItem { get; set; }

        ObservableCollection<string> items;
        public ObservableCollection<string> ItemSource { get { return items; } set { items = value; RaisePropertyChanged(); } }

        string search;
        public string SearchQuery { get { return search; } set { search = value; Search(); RaisePropertyChanged(); } }


        public delegate void EventHandler();

        public event EventHandler ItemSelectedChanged; //  event handler when a item is selected

        public bool IsCountry { get; set; }

        List<Country> Countries { get; set; } = new List<Country>();

        List<Models.DataObjects.State> States { get; set; } = new List<Models.DataObjects.State>();


        async void Search()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                ItemSource = new ObservableCollection<string>();
                return;
            }

            if (IsCountry)
            {
                var result = await StoreManager.CountryStore.Search(SearchQuery.Trim());

                if (result != null && result.Any())
                {
                    Countries = result.ToList();

                    try
                    {
                        ItemSource = new ObservableCollection<string>(result.Select( x => x.Name ));
                    }
                    catch (Exception)
                    {
                        ItemSource = new ObservableCollection<string>();
                    }
                }
                else
                {
                    Countries = new List<Country>();
                    ItemSource = new ObservableCollection<string>();
                }

            }
            else
            {
                var result = await StoreManager.StateStore.Search(SearchQuery.Trim());

                if (result != null && result.Any())
                {
                    States = result.ToList();

                    try
                    {
                        ItemSource = new ObservableCollection<string>(result.Select(x => x.Name));
                    }
                    catch (Exception)
                    {
                        ItemSource = new ObservableCollection<string>();
                    }
                }
                else
                {
                    States = new List<Models.DataObjects.State>();
                    ItemSource = new ObservableCollection<string>();
                }
            }

        }

        public Command Done => new Command(() =>
        {
            if(IsCountry)
            {
                var item = Countries.Where(x => x.Name == (string)SelectedItem ).First();
                SelectedItem = item;
            }
            else
            {
                var item = States.Where(x => x.Name == (string)SelectedItem).First();
                SelectedItem = item;
            }

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
