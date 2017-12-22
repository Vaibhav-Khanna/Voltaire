﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using voltaire.Models;
using voltaire.PageModels.Base;
using Xamarin.Forms;
using System.Linq;
using FreshMvvm;

namespace voltaire.PageModels
{
    public class QuotationsPageModel : BasePageModel
    {

        public QuotationsPageModel()
        {
            FilterTypes = new ObservableCollection<string>() { "All", "Name", "Status" };

            Filter = 0;
        }

		Partner customer;
		public Partner Customer
		{
			get { return customer; }
			set
			{
				customer = value;
                RaisePropertyChanged();
               
			}
		}

        ObservableCollection<string> filtertypes;
        public ObservableCollection<string> FilterTypes 
        {
            get { return filtertypes; }
            set 
            {
                filtertypes = value;
                RaisePropertyChanged();
            }
        }


        int filter { get; set; }
        public int Filter 
        {
            get { return filter; }
            set 
            {
                filter = value; 
                RaisePropertyChanged();
            }
        }

        public Command AddQuotation => new Command(async (object NavigationService) =>
       {
            await ((IPageModelCoreMethods)NavigationService).PushPageModel<QuotationDetailViewPageModel>(new Tuple<Partner,bool,QuotationsModel>(customer,true,null));
       });


        public Command FilterTap => new Command(() =>
       {

       });

		public Command SearchQuery => new Command(() =>
	  {
            SearchResults((SearchText));
	  });

        public Command TapQuotation => new Command(async (object obj) =>
       {
            var item = obj as Tuple<IPageModelCoreMethods, QuotationsModel>;
            await item.Item1.PushPageModel<QuotationDetailViewPageModel>(new Tuple<Partner, bool, QuotationsModel>(customer, false, item.Item2));
       });

        public string SearchText { get; set; }

        ObservableCollection<QuotationsModel> all_items;

        ObservableCollection<QuotationsModel> quotationsitemsource;
        public ObservableCollection<QuotationsModel> QuotationsItemSource 
        {
            get { return quotationsitemsource; }
            set 
            {
                quotationsitemsource = value;
                RaisePropertyChanged();
            }
        } 


        void SearchResults(string query_string)
        {
            if (all_items.Count == 0)
                return;

            List<QuotationsModel> items = new List<QuotationsModel>();

            if(string.IsNullOrWhiteSpace(query_string))
            {
                QuotationsItemSource = all_items;
                return;
            }

            query_string = query_string.Trim().ToLower();

            try
            {

                switch (Filter)
                {
                    case 0:
                        {
                            items = all_items.Where((arg) => arg.Name.ToLower().Contains(query_string) || arg.Date.ToString().ToLower().Contains(query_string) || arg.Status.ToLower().Contains(query_string) || arg.TotalAmount.ToString().Contains(query_string) || arg.Ref.ToLower().Contains(query_string)).ToList();
                            break;
                        }
                    case 1:
                        {
                            items = all_items.Where((arg) => arg.Name.ToLower().Contains(query_string)).ToList();
                            break;
                        }
                    case 2:
                        {
                            items = all_items.Where((arg) => arg.Status.ToLower().Contains(query_string)).ToList();
                            break;
                        }
                    default:
                        {
                            items = all_items.Where((arg) => arg.Name.ToLower().Contains(query_string) || arg.Date.ToString().ToLower().Contains(query_string) || arg.Status.ToLower().Contains(query_string) || arg.TotalAmount.ToString().Contains(query_string) || arg.Ref.ToLower().Contains(query_string)).ToList();
                            break;
                        }

                }
            }
            catch(Exception)
            {
                QuotationsItemSource = all_items;
            }

            if(items != null)
            { 
                QuotationsItemSource = new ObservableCollection<QuotationsModel>(items); 
            }

        }


        public override void Init(object initData)
        {
            base.Init(initData);

			var context = initData as Partner;

			if (context == null)
				return;

            Customer = context;

            FetchItems();
        }

        public override void TabAppearing()
        {
            base.TabAppearing();

            FetchItems();
        }

        async void FetchItems()
        {            
            var items = await StoreManager.SaleOrderStore.GetQuotationItemsByCustomer(Customer.ExternalId);

            List<QuotationsModel> Quotations = new List<QuotationsModel>();

            foreach (var item in items)
            {
                Quotations.Add(new QuotationsModel(item));
            }

           
            foreach (var item in Quotations)
            {
                item.BackColor = Quotations.IndexOf(item) % 2 == 0 ? Color.FromRgb(247, 247, 247) : Color.White;
            }


            all_items = new ObservableCollection<QuotationsModel>(Quotations);
            QuotationsItemSource = all_items;

            SearchQuery.Execute(null);
        }

    }
}
