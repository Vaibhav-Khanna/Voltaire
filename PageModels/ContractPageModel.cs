using System;
using System.Collections.ObjectModel;
using voltaire.Models;
using voltaire.PageModels.Base;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;

namespace voltaire.PageModels
{
    public class ContractPageModel : BasePageModel
    {
        Customer customer;
        public Customer Customer 
        { 
            get { return customer; }
            set
            {
                customer = value;


                List<ContractModel> contract_list = new List<ContractModel>();

                foreach (var item in customer.Contracts)
                {
                    contract_list.Add(new ContractModel(item){ BackColor = contract_list.Count%2 == 0? Color.White : Color.FromRgb(247,247,247) });
                }

                all_items = new ObservableCollection<ContractModel>(contract_list);

                ContractsItemSource = all_items;

            }
        }

        public Command AddContract => new Command( async(obj) =>
       {
            var navigation = obj as FreshMvvm.IPageModelCoreMethods;
            await navigation.PushPageModel<NewContractPageModel>(new Tuple<Customer,Contract>(Customer,null));
       });

        public Command ItemTapped => new Command( async(obj) =>
       {
            var navigation = obj as Tuple<FreshMvvm.IPageModelCoreMethods, Contract>;
            await navigation.Item1.PushPageModel<NewContractPageModel>(new Tuple<Customer,Contract>(Customer,navigation.Item2));
       });


        public Command SearchQuery => new Command((obj) =>
       {
            SearchResults(SearchText);
       });

        public string SearchText 
        {
            get;
            set;
        }

        ObservableCollection<ContractModel> all_items;

        ObservableCollection<ContractModel> contractsitemsource;
        public ObservableCollection<ContractModel> ContractsItemSource
        {
            get { return contractsitemsource; }
            set 
            {
                contractsitemsource = value;
                RaisePropertyChanged();
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


		void SearchResults(string query_string)
		{
			if (all_items.Count == 0)
				return;

			List<ContractModel> items = new List<ContractModel>();

			if (string.IsNullOrWhiteSpace(query_string))
			{
				ContractsItemSource = all_items;	
				return;
			}

			query_string = query_string.Trim();

			try
			{
                
                items = all_items.Where((arg) => arg.Name.ToLower().Trim().Contains(query_string.ToLower().Trim())).ToList();
							
			}
			catch (Exception ex)
			{
                
			}

			if (items != null)
			{
				ContractsItemSource = new ObservableCollection<ContractModel>(items);
			}


		}



	}
}
