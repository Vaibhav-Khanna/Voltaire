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
using System.Diagnostics;

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

        private IPartnerStore CustomerStore => StoreManager.CustomerStore;

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
            CustomerStore.GradeFilter = null;
            CustomerStore.WeightFilter = null;

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

        private ObservableCollection<PartnerGrade> grades;
        public ObservableCollection<PartnerGrade> partnerGrades 
        { 
            get { return grades; }
            set { grades = value; RaisePropertyChanged(); }
        }

        //First call to populate the list of customers
        async void Get()
        {
            // Local data
            IsLoading = true;
                      
            var result = await CustomerStore.GetItemsAsync(false);

            // Server refresh
            if ( result == null || !result.Any() )
            {
                LoadingText = AppResources.FetchingData;
                result = await CustomerStore.GetItemsAsync(true);
                CreateGroupedCollection(result);
                LoadingText = AppResources.FetchingData;
            }
            else
                CreateGroupedCollection(result);

            IsLoading = false;

        }

        bool Loadingmore = false;


        //Fetch more contacts for infinite scroll
        public Command LoadMore => new Command(async () =>
        {
            if (!IsLoadMore || Loadingmore)
                return;

          if (string.IsNullOrWhiteSpace(SearchText))
          {
                Loadingmore = true;
                
                var result = await CustomerStore.GetNextItemsAsync(Customers.Count);

                if (result != null && result.Any())
                {
                    var list = customers.ToList();
                    list.AddRange(result);

                    Debug.WriteLine("Fetched "+result.Count());

                    if ((result as IQueryResultEnumerable<Partner>) != null)
                    {
                        var totalCount = (result as IQueryResultEnumerable<Partner>).TotalCount;

                        if(totalCount != 1)
                        CustomersCount = totalCount.ToString() + " " + AppResources.Contacts;
                        else
                            CustomersCount = totalCount.ToString() + " " + AppResources.Contact;

                        if (!string.IsNullOrWhiteSpace(SearchText))
                            IsLoadMore = false;
                        else
                        {
                            if (Convert.ToInt32(totalCount) - list.Count() > 0)
                                IsLoadMore = true;
                            else
                                IsLoadMore = false;
                        }
                    }
                     
                    CreateGroupedCollection(list);
                }
                else
                    IsLoadMore = false;

                Loadingmore = false;
          }
      });


        public Command AddContact => new Command( async() =>
       {
            await CoreMethods.PushPageModel<ContactAddPageModel>();
       });


        public Command RefreshList => new Command(async (obj) =>
       {
           if (!string.IsNullOrWhiteSpace(SearchText))
           {
               IsRefreshing = false;
               return;
           }

           IsLoading = true;
           IsLoadingText = AppResources.Refreshing;
           var result = await CustomerStore.GetItemsAsync(true);
           CreateGroupedCollection(result);
           IsRefreshing = false;
           IsLoading = false;
       });


        public Command SearchContact => new Command(async () =>
       {
           if (string.IsNullOrWhiteSpace(searchtext))
           {
               var res = await CustomerStore.GetItemsAsync(false);
               CreateGroupedCollection(res);
               return;
           }

          
           var result = await CustomerStore.Search(SearchText.Trim());
           CreateGroupedCollection(result);
           CustomersCount += " " + AppResources.MatchingSearch;
       });


        public Command FilterByWeight => new Command( (obj) =>
       {
           CustomerStore.WeightFilter = (int?) Convert.ToInt32(obj);
           Get();
       });


        private void CreateGroupedCollection(IEnumerable<Partner> list)
        {
            
            if (list == null)
                list = new List<Partner>();


            if( (list as IQueryResultEnumerable<Partner>) != null )
            {
                var totalCount = (list as IQueryResultEnumerable<Partner>).TotalCount;
               
                if (totalCount != 1)
                    CustomersCount = totalCount.ToString() + " " + AppResources.Contacts;
                else
                    CustomersCount = totalCount.ToString() + " " + AppResources.Contact;

                if (!string.IsNullOrWhiteSpace(SearchText))
                    IsLoadMore = false;
                else
                {
                    if (Convert.ToInt32(totalCount) - list.Count() > 0)
                        IsLoadMore = true;
                    else
                        IsLoadMore = false;
                }
            }

            list = list.OrderBy( (arg) => arg.Name );

            Customers = new ObservableCollection<Partner>(list);                   

            var models = Customers.Select(i => new CustomerModel(i) { navigation = CoreMethods }).ToList();

            var groupedData =
                models
                    .GroupBy(p => p.NameSort)
                    .Select(p => new ObservableGroupCollection<string, CustomerModel>(p))
                    .ToList();

            CustomersItems = new ObservableCollection<ObservableGroupCollection<string, CustomerModel>>(groupedData);

        }


        //INIT data form page  freshmvvm
        public async override void Init(object initData)
        {
             //columnSpan for listview 
            _listColumnSpan = 2;
          
            //filters view
            _filterLayoutVisibility = false;

            //filter frame image 
            _filterImage = "filters";

            var _grades = await StoreManager.PartnerGradeStore.GetItemsAsync();
            //PartnerGrades 
            partnerGrades = new ObservableCollection<PartnerGrade>(_grades?.Select((arg) => new PartnerGrade() { Grade = arg.Name }));

        }


        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);

            if(returnedData!=null)
            {
                IsRefreshing = true;
            }

        }


    }
    
}
