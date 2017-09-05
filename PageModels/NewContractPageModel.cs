using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FreshMvvm;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using voltaire.Models;
using Xamarin.Forms;


namespace voltaire.PageModels
{
    public class NewContractPageModel : FreshBasePageModel
    {
        Customer customer;
        public Customer Customer 
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

                if (contract.Agreements == null)
				{
					List<Agreement> agreements = new List<Agreement>();

					foreach (var item in ProductConstants.Agreements)
					{
						agreements.Add(item.ObjectClone());
					}

                    contract.Agreements = agreements;
                }
                else
                {
                   
                }

				List<AgreementModel> agreementmodel = new List<AgreementModel>();

				foreach (var item in contract.Agreements)
				{
					agreementmodel.Add(new AgreementModel(item));
				}

				AgreementItemSource = new ObservableCollection<AgreementModel>(agreementmodel);

                OrderN = contract.Name;

                Subject = contract.Subject;

                DateFrom = contract.DateFrom;

                DateTo = contract.DateTo;

                RaisePropertyChanged();
            }
        }


        public Command BackButton => new Command( async(obj) =>
       {
            await CoreMethods.PopPageModel();
       });


        public Command ItemTapped => new Command((obj) =>
       {
            var agreement = obj as AgreementModel; 
            agreement.IsSelected = !agreement.IsSelected;
       });


        public Command CreatePDF => new Command(async(obj) =>
       {
		   using (PdfDocument document = new PdfDocument())
		   {
			   //Add a page in the PDF document.
			   PdfPage page = document.Pages.Add();
			   //Access the PDF graphics instance of the page.
			   PdfGraphics graphics = page.Graphics;
			   
                //Create the PDF font instance.
			   PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);
			 
                //Draw the text in PDF page.
			   graphics.DrawString("!", font, PdfBrushes.Black, new PointF(20, 20));
               //Save the PDF document to disk

               await CoreMethods.PushPageModel<ContractPDFViewingPageModel>(document);
		   }
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
                contract.Name = ordern;
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
                contract.DateFrom = datefrom;
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
                contract.DateTo = dateto;
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

            var context = initData as Tuple<Customer,Contract>;

            Customer = context.Item1;

            if(context.Item2==null)
            {
                var _contract = new Contract(){ ModifiedDateTime = DateTime.Now };
                customer.Contracts.Add(_contract);
                Contract = _contract;
				NewContract = $"New contract for {customer.FirstName} {customer.LastName}";
            }
            else
            {
                Contract = context.Item2;
				NewContract = "Contract " + contract.Name;
            }

        }
    }
}
