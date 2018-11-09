using System;
using System.IO;
using System.Reflection;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using voltaire.Models;
using voltaire.PageModels.Base;
using Xamarin.Forms;
using System.Linq;
using voltaire.Helpers;
using System.Text;
using System.Collections.Generic;
using Syncfusion.Pdf.Parsing;
using voltaire.DataStore.Implementation;
using voltaire.Resources;

namespace voltaire.PageModels
{
    public class ContractPDFViewingPageModel : BasePageModel
    {

        List<AgreementModel> agreements;
        string ContractTemplatePDF;
        PdfDocument document;


        public Command BackButton => new Command( async(obj) =>
       {
            await CoreMethods.PopPageModel();
       });

        public Command SignPDF => new Command(async (obj) =>
		{
            if(Contract.ToSend)
            {
                await CoreMethods.DisplayAlert(AppResources.Alert,AppResources.AlreadySigned,AppResources.Ok);
                return;
            }

            await CoreMethods.PushPageModel<ContractSignValidatePageModel>(Contract);
		});


        Stream m_pdfDocumentStream;
		public Stream PdfDocumentStream
		{
			get
			{
				return m_pdfDocumentStream;
			}
			set
			{
				m_pdfDocumentStream = value;
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

                Title = contract.OrderNumber;

                RaisePropertyChanged();
            }
        }

        string title;
        public string Title 
        {
            get { return title; }
            set
            {
                title = value;
                RaisePropertyChanged();
            }
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            var _contract = initData as Tuple<Contract, string, List<AgreementModel>>;

            if(_contract!=null)
            {
                ContractTemplatePDF = _contract.Item2;

                agreements = _contract.Item3;

                Contract = _contract.Item1;

                GeneratePDF();
            }
        }


        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);

            if (returnedData is Contract)
            {
                Contract = returnedData as Contract;

                GeneratePDF();    
            }
        }


        async void GeneratePDF()
        {

            var pdf = await Helpers.PclStorage.LoadFileLocal(StorageKeys.SaleContract);

            if (pdf != null)
            {
                //Load the PDF document.

                PdfLoadedDocument loadedDocument = new PdfLoadedDocument(pdf);

                //Create a new PDF document.
                document = new PdfDocument();

                int startIndex = 0;

                int endIndex = loadedDocument.Pages.Count - 1;

                //Import all the pages to the new PDF document.
                document.ImportPageRange(loadedDocument, startIndex, endIndex);

                MemoryStream m = new MemoryStream();

                document.Save(m);

                //Close both document instances.
                //loadedDocument.Close(true);

                Contract.Document = document;

                PdfDocumentStream = m;
            }

        }




	}
}
