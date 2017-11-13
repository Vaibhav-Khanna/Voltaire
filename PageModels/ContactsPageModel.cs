using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using FreshMvvm;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.DataStore.Implementation;
using voltaire.DataStore.Implementation.Stores;
using voltaire.Helpers.Collections;
using voltaire.Models;
using voltaire.PageModels.Base;
using Xamarin.Forms;
using voltaire.Resources;
using System.Reflection;
using Microsoft.WindowsAzure.MobileServices;

namespace voltaire.PageModels
{

    public class ContactsPageModel : BasePageModel
    {

        string customersCount;
        public string CustomersCount
        {
            get { return customersCount; }
            set
            {
                customersCount = value;
                RaisePropertyChanged();
            }
        }

        string searchtext;
        public string SearchText
        {
            get { return searchtext; }
            set
            {
                searchtext = value;
                RaisePropertyChanged();
                SearchContact.Execute(null);
            }
        }


        ObservableCollection<Partner> customers;
        public ObservableCollection<Partner> Customers
        {
            get { return customers; }
            set
            {
                customers = value;
                RaisePropertyChanged();
            }
        }

        ObservableCollection<ObservableGroupCollection<string, CustomerModel>> customersitems;
        public ObservableCollection<ObservableGroupCollection<string, CustomerModel>> CustomersItems
        {
            get { return customersitems; }
            set
            {
                customersitems = value;
                RaisePropertyChanged();
            }
        }

        public ICommand FiltersLayoutCommand => new Command(FiltersLayoutAppearing);

        private ICustomerStore Store => StoreManager.CustomerStore;

        private int? _listColumnSpan;

        public int? listColumnSpan
        {

            get { return _listColumnSpan; }

            set { _listColumnSpan = value; RaisePropertyChanged("listColumnSpan"); }
        }

        private bool _filterLayoutVisibility;

        public bool filterLayoutVisibility
        {

            get { return _filterLayoutVisibility; }

            set { _filterLayoutVisibility = value; RaisePropertyChanged("filterLayoutVisibility"); }
        }

        private string _filterImage;

        public string filterImage
        {

            get { return _filterImage; }

            set { _filterImage = value; RaisePropertyChanged("filterImage"); }
        }

        public ContactsPageModel()
        {
            Get();
        }

        private void FiltersLayoutAppearing()
        {
            if (listColumnSpan == 2)
            {
                listColumnSpan = 1;
                filterLayoutVisibility = true;
                filterImage = "close";
            }
            else if (listColumnSpan == 1)
            {
                listColumnSpan = 2;
                filterLayoutVisibility = false;
                filterImage = "filters";
            }

        }

        public ObservableCollection<PartnerGrade> partnerGrades { get; set; }


        async void Get()
        {
            // Local data
            var result = await Store.GetItemsAsync(false);
            CreateGroupedCollection(result);

            // Server refresh
            IsRefreshing = true;
            result = await Store.GetItemsAsync(true);
            CreateGroupedCollection(result);
            IsRefreshing = false;
        }

        public Command LoadMore => new Command(async () =>
      {
          if (string.IsNullOrWhiteSpace(SearchText))
          {
              var result = await Store.GetNextItemsAsync(Customers.Count);

              if (result != null && result.Count() != 0)
              {
                  var list = customers.ToList();
                  list.AddRange(result);
                  CreateGroupedCollection(list);
              }
          }
      });


        public Command AddContact => new Command(() =>
       {
           
       });


        public Command RefreshList => new Command(async (obj) =>
       {
           var result = await Store.GetItemsAsync(true);
           CreateGroupedCollection(result);
           IsRefreshing = false;
       });



        public Command SearchContact => new Command(async () =>
       {
           if (string.IsNullOrWhiteSpace(searchtext))
           {
               var res = await Store.GetItemsAsync(false);
               CreateGroupedCollection(res);
               return;
           }

           var result = await Store.Search(SearchText.Trim());
           CreateGroupedCollection(result);
       });


        private void CreateGroupedCollection(IEnumerable<Partner> list)
        {
            if (list == null)
                list = new List<Partner>();

          
            if( (list as IQueryResultEnumerable<Partner>) != null )
            {
                CustomersCount = (list as IQueryResultEnumerable<Partner>).TotalCount.ToString() + " " + AppResources.Contacts;
            }         

            Customers = new ObservableCollection<Partner>(list);                   

            var models = Customers.Select(i => new CustomerModel(i) { navigation = CoreMethods }).ToList();

            var groupedData =
                models.OrderBy(p => p.Customer.Name)
                    .GroupBy(p => p.NameSort)
                    .Select(p => new ObservableGroupCollection<string, CustomerModel>(p))
                    .ToList();

            CustomersItems = new ObservableCollection<ObservableGroupCollection<string, CustomerModel>>(groupedData);
        }


        //INIT data form page  freshmvvm
        public override void Init(object initData)
        {

            //columnSpan for listview
            _listColumnSpan = 2;
            //filters view
            _filterLayoutVisibility = false;
            //filter frame image 
            _filterImage = "filters";

            //PartnerGrades 
            partnerGrades = new ObservableCollection<PartnerGrade>
            {
                new PartnerGrade {Grade="CCE"},
                new PartnerGrade {Grade="CSO"},
                new PartnerGrade {Grade="DRE"},
                new PartnerGrade {Grade="Endurance"},
                new PartnerGrade {Grade="PRO"},
                new PartnerGrade {Grade="Particulier"},
                new PartnerGrade {Grade="Ecuries"}
            };

        }

    }
    
}
