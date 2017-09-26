using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using voltaire.Models;
using voltaire.PageModels.Base;
using Xamarin.Forms;

namespace voltaire.PageModels
{
    public class ContractsMainPageModel : BasePageModel
    {
        public ContractsMainPageModel()
        {
        }

		public Command AddContract => new Command(async (obj) =>
		{			
            await CoreMethods.PushPageModel<NewContractPageModel>(new Tuple<Customer, Contract>(null, null));
		});

		public Command ItemTapped => new Command(async (obj) =>
		{
			var contract = obj as Contract;
            await CoreMethods.PushPageModel<NewContractPageModel>(new Tuple<Customer, Contract>(contract.Customer, contract));
		});

        	
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


            // Mock Data

            var list = new List<Contract>();
            list.Add(new Contract(new Customer(){ FirstName = "Johnny", LastName = "Layfield" }){ Name = "New Dummy Contract" ,ModifiedDateTime = DateTime.Now  });
            list.Add(new Contract(new Customer() { FirstName = "Nicki", LastName = "Semtosa" }) { Name = "New Dummy Contract", ModifiedDateTime = DateTime.Now });
            var Mock_list = new List<ContractModel>();
            foreach (var item in list)
            {
                Mock_list.Add(new ContractModel(item));
            }

			// Mock Data

			ContractsItemSource = new ObservableCollection<ContractModel>(Mock_list);

		}

	}
}
