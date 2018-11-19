using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using FreshMvvm;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using voltaire.Models;
using voltaire.PageModels.Base;
using voltaire.Resources;
using Xamarin.Forms;
using System.Linq;
using voltaire.DataStore.Implementation;

namespace voltaire.PageModels
{
    public class NewContractPageModel : BasePageModel
    {

        string ContractTemplate;

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

        Contract contract;
        public Contract Contract
        {
            get { return contract; }
            set
            {
                contract = value;
                RaisePropertyChanged();
            }
        }


        public Command BackButton => new Command(async (obj) =>
      {
          Dialog.ShowLoading("");

          await StoreManager.ContractStore.UpdateAsync(contract);

          Dialog.HideLoading();

          await CoreMethods.PopPageModel(contract);
      });


        public Command ItemTapped => new Command((obj) =>
       {
            var agreement = obj as AgreementModel; 
            //agreement.IsSelected = !agreement.IsSelected;
       });


        public Command CreatePDF => new Command(async(obj) =>
       {
            if(string.IsNullOrWhiteSpace(OrderN))
            {
                await CoreMethods.DisplayAlert(AppResources.FillInformation,AppResources.EnterOrderNumber ,AppResources.Ok);
               return;
            }

           if (string.IsNullOrWhiteSpace(ContractTemplate))
               return;

            await CoreMethods.PushPageModel<ContractPDFViewingPageModel>(new Tuple<Contract,string,List<AgreementModel>>(Contract,ContractTemplate,AgreementItemSource.ToList()));
       });


        string newcontract;
        public string NewContract 
        {
            get { return newcontract; }
            set
            {
                newcontract = value;
                RaisePropertyChanged();
            }
        }

		string ordern;
		public string OrderN
		{
			get { return ordern; }
			set
			{
				ordern = value;
                contract.OrderNumber = ordern;
                NewContract = ordern;
				RaisePropertyChanged();
			}
		}

		string subject;
		public string Subject
		{
			get { return subject; }
			set
			{
				subject = value;
                contract.Subject = subject;
				RaisePropertyChanged();
			}
		}

        DateTime? datefrom;
        public DateTime? DateFrom
        {
            get { return datefrom; }
            set 
            {
                datefrom = value;
                contract.PeriodBegin = datefrom;
                RaisePropertyChanged();
            }
        }

		DateTime? dateto;
		public DateTime? DateTo
		{
			get { return dateto; }
			set
			{
				dateto = value;
                contract.PeriodEnd = dateto;
				RaisePropertyChanged();
			}
		}

        ObservableCollection<AgreementModel> agreementitemsource;
        public ObservableCollection<AgreementModel> AgreementItemSource 
        { 
            get { return agreementitemsource; }
            set
            {
                agreementitemsource = value;
                RaisePropertyChanged();
            }
        }


        public override void Init(object initData)
        {
            base.Init(initData);

            var context = initData as Tuple<Partner,Contract>;

            Customer = context.Item1;

            if (Customer == null)
                return;

            if(context.Item2==null)
            {
                var _contract = new Contract(){ PartnerId = Customer.ExternalId, PeriodBegin = DateTime.Now, PeriodEnd = DateTime.Now.AddDays(7) };
                               
                Contract = _contract;

                StoreManager.ContractStore.InsertAsync(_contract);

                NewContract = $"{AppResources.NewContractFor} {customer.Name}";
            }
            else
            {
                Contract = context.Item2;
				
                NewContract = contract.OrderNumber;

                OrderN = contract.OrderNumber;
            }

            GetAgreementData();

            Subject = contract.Subject;
            
            DateFrom = contract.PeriodBegin;

            DateTo = contract.PeriodEnd;

        }

        async void GetAgreementData()
        {
            var conditions = await StoreManager.ContractStore.GetTermsConditionsOfContract();

            ContractTemplate = await Helpers.PclStorage.IsFileExistAsync(StorageKeys.SaleContract) ? "exists" : null;

            List<AgreementModel> agreementmodel = new List<AgreementModel>();

            if (conditions != null && conditions.Any())
            {
                foreach (var item in conditions)
                {
                    agreementmodel.Add(new AgreementModel(new Agreement() { ContractString = new AgreementText(){ Description = item, Title = item }, Title = item, IsSelected = true }));
                } 
            }

            AgreementItemSource = new ObservableCollection<AgreementModel>(agreementmodel);
        }

    }
}
