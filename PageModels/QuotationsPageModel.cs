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

		Partner customer;

		public Partner Customer
		{
			get { return customer; }
			set
			{
				customer = value;

                filtertypes = new ObservableCollection<string>() { "All", "Name", "Status" };
              
                filter = 0;

				List<QuotationsModel> not_sent_quotations = new List<QuotationsModel>();

				if (customer.Quotations != null)
					not_sent_quotations = customer.Quotations.Where((arg) => arg.Status != QuotationStatus.Sent).ToList();


                foreach (var item in not_sent_quotations)
                {
                    item.BackColor = customer.Quotations.IndexOf(item)%2 == 0 ?  Color.FromRgb(247,247,247) : Color.White;
                }


                all_items = new ObservableCollection<QuotationsModel>(not_sent_quotations);
                quotationsitemsource = all_items;
                   
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(FilterTypes));
                RaisePropertyChanged(nameof(Filter));
                RaisePropertyChanged(nameof(QuotationsItemSource));

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
                quotationsitemsource = all_items;
				RaisePropertyChanged(nameof(QuotationsItemSource));
                return;
            }

			query_string = query_string.Trim();

            try
            {

                switch (Filter)
                {
                    case 0:
                        {
                            items = all_items.Where((arg) => arg.Name.ToLower().Contains(query_string.ToLower()) || arg.Date.ToString().ToLower().Contains(query_string.ToLower()) || arg.Status.ToString().ToLower().Contains(query_string.ToLower()) || arg.TotalAmount.ToString().Contains(query_string) || arg.Ref.Contains(query_string)).ToList();
                            break;
                        }
                    case 1:
                        {
                            items = all_items.Where((arg) => arg.Name.ToLower().Contains(query_string.ToLower())).ToList();
                            break;
                        }
                    case 2:
                        {
                            items = all_items.Where((arg) => arg.Status.ToString().ToLower().Contains(query_string.ToLower())).ToList();
                            break;
                        }
                    default:
                        {
                            items = all_items.Where((arg) => arg.Name.ToLower().Contains(query_string.ToLower()) || arg.Date.ToString().ToLower().Contains(query_string.ToLower()) || arg.Status.ToString().ToLower().Contains(query_string.ToLower()) || arg.TotalAmount.ToString().Contains(query_string) || arg.Ref.Contains(query_string)).ToList();
                            break;
                        }

                }
            }
            catch(Exception ex)
            {
				//quotationsitemsource = all_items;
				//RaisePropertyChanged(nameof(QuotationsItemSource));
				//return;
            }

            if(items != null)
            { 
                quotationsitemsource = new ObservableCollection<QuotationsModel>(items);
                RaisePropertyChanged(nameof(QuotationsItemSource));
            }


        }


        public override void Init(object initData)
        {
            base.Init(initData);

			var context = initData as Partner;

			if (context == null)
				return;

            Customer = context;
        }


    }
}
