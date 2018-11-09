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
        Partner customer;
        public Partner Customer 
        { 
            get { return customer; }
            set
            {
                customer = value;
            }
        }

        public Command AddContract => new Command( async(obj) =>
       {
            var navigation = obj as FreshMvvm.IPageModelCoreMethods;

            await navigation.PushPageModel<NewContractPageModel>(new Tuple<Partner,Contract>(Customer,null));
       });


        public Command ItemTapped => new Command( async(obj) =>
       {
            var navigation = obj as Tuple<FreshMvvm.IPageModelCoreMethods, Contract>;

            await navigation.Item1.PushPageModel<NewContractPageModel>(new Tuple<Partner,Contract>(Customer,navigation.Item2));
       });


        public Command SearchQuery => new Command((obj) =>
       {
            SearchResults(SearchText);
       });

        public string SearchText { get; set; }

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

            var context = initData as Partner;

            if (context == null)
                return;

            Customer = context;

            GetData();
        }

        public override void TabAppearing()
        {
            base.TabAppearing();

            if(contractsitemsource!=null && customer!=null)
            GetData();
        }

        async void GetData()
        {
            var items = await StoreManager.ContractStore.GetContractsByPartnerExternalId(customer.ExternalId);

            List<ContractModel> contract_list = new List<ContractModel>();

            if (items != null)
                foreach (var item in items)
                {
                    contract_list.Add(new ContractModel(item) { CustomerName = Customer.Name, BackColor = contract_list.Count % 2 == 0 ? Color.White : Color.FromRgb(247, 247, 247) });
                }


            ContractsItemSource = new ObservableCollection<ContractModel>(contract_list);

            all_items = contractsitemsource;
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
			catch (Exception)
			{
                
			}

			if (items != null)
			{
				ContractsItemSource = new ObservableCollection<ContractModel>(items);
			}
		}



	}
}
