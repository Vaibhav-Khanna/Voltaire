using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using voltaire.Models;
using voltaire.PageModels.Base;
using Xamarin.Forms;
using System.Linq;

namespace voltaire.PageModels
{
    public class QuotationsPageModel : BasePageModel
    {

		Customer customer;

		public Customer Customer
		{
			get { return customer; }
			set
			{
				customer = value;


                filtertypes = new ObservableCollection<string>() { "All", "Name", "Status", "Amount", "Date" };
              
                filter = 0;

                if(customer.Quotations!=null)
                foreach (var item in customer.Quotations)
                {
                    item.BackColor = customer.Quotations.IndexOf(item)%2 == 0 ?  Color.FromRgb(247,247,247) : Color.White;
                }


                all_items = new ObservableCollection<QuotationsModel>(customer.Quotations);
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
           await ((FreshMvvm.IPageModelCoreMethods)NavigationService).PushPageModel<QuotationDetailViewPageModel>();
       });


        public Command FilterTap => new Command(() =>
       {

       });

		public Command SearchQuery => new Command(() =>
	  {
            SearchResults((SearchText));
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

            switch (Filter)
            {
                case 0:
                    {
                        items = all_items.Where((arg) => arg.Name.ToLower().Contains(query_string.ToLower()) || arg.Date.ToString().ToLower().Contains(query_string.ToLower()) || arg.Status.ToString().ToLower().Contains(query_string.ToLower()) || arg.TotalAmount.ToString().Contains(query_string) || arg.Ref.Contains(query_string) ).ToList();
                        break;
                    }
                case 1 : 
                    {
                        items = all_items.Where((arg) => arg.Name.ToLower().Contains(query_string.ToLower())).ToList();
                        break;
                    }
                default:
					{
                        items = all_items.Where((arg) => arg.Name.ToLower().Contains(query_string.ToLower()) || arg.Date.ToString().ToLower().Contains(query_string.ToLower()) || arg.Status.ToString().ToLower().Contains(query_string.ToLower()) || arg.TotalAmount.ToString().Contains(query_string) || arg.Ref.Contains(query_string)).ToList();
						break; 
                    }
                   
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

			var context = initData as Customer;

			if (context == null)
				return;

            Customer = context;

        }


    }
}
