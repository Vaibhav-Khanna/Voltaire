using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Rg.Plugins.Popup.Services;
using voltaire.Models;
using voltaire.PageModels.Base;
using voltaire.PopUps;
using Xamarin.Forms;

namespace voltaire.PageModels
{
    public class ContractsMainPageModel : BasePageModel
    {

        CustomerPickerPopupModel popup_context = new CustomerPickerPopupModel(); //  Popup picker model 



		public Command AddContract => new Command(async (obj) =>
		{
			popup_context.ItemSelectedChanged += Popup_Context_ItemSelectedChanged;    // Subscribe to the event
         
            await PopupNavigation.PushAsync(new CustomerPickerPopUp() { BindingContext = popup_context }, true);
		});


        async void Popup_Context_ItemSelectedChanged()
        {
			if (popup_context.SelectedItem != null)
			{	
                await CoreMethods.PushPageModel<NewContractPageModel>(new Tuple<Partner, Contract>(popup_context.SelectedItem, null));
			}
			// Unsubscribe from the event
			popup_context.ItemSelectedChanged -= Popup_Context_ItemSelectedChanged;
        }



        public Command ItemTapped => new Command(async (obj) =>
		{
			var contract = obj as Contract;

            var partner = await StoreManager.CustomerStore.GetItemByExternalId(contract.PartnerId);

            await CoreMethods.PushPageModel<NewContractPageModel>(new Tuple<Partner,Contract>(partner, contract));
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
         
            var Mock_list = new List<ContractModel>();
          
            foreach (var item in list)
            {
                Mock_list.Add(new ContractModel(item));
            }

            var list_customer = new List<Partner>();

            foreach (var item in list)
            {
               
            }
            popup_context.ItemSource = new ObservableCollection<Partner>(list_customer);

            // Mock Data

            ContractsItemSource = new ObservableCollection<ContractModel>(Mock_list);

		}

	}
}
