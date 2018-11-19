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

        AddCustomerPopUpModel popup_context; //  Popup picker model 

        public Command AddContract => new Command(async (obj) =>
		{
            popup_context = new AddCustomerPopUpModel(false);

            popup_context.ItemSelectedChanged += Popup_Context_ItemSelectedChanged;    // Subscribe to the event

            await PopupNavigation.PushAsync(new AddCustomerPopUp() { BindingContext = popup_context }, true);
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

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            GetData();
        }

        async void GetData()
        {
            var items = await StoreManager.ContractStore.GetItemsAsync(false, true);

            List<ContractModel> contract_list = new List<ContractModel>();

            if (items != null)
                foreach (var item in items)
                {
                    contract_list.Add(new ContractModel(item) { BackColor = contract_list.Count % 2 == 0 ? Color.White : Color.FromRgb(247, 247, 247) });
                }

            ContractsItemSource = new ObservableCollection<ContractModel>(contract_list);
        }

    }
}
