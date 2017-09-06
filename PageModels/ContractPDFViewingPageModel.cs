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

namespace voltaire.PageModels
{
    public class ContractPDFViewingPageModel : BasePageModel
    {

        public Command BackButton => new Command( async(obj) =>
       {
            await CoreMethods.PopPageModel();
       });

        public Command SignPDF => new Command(async (obj) =>
		{
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

                Title = contract.Name;

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

            var _contract = initData as Contract;

            if(_contract!=null)
            {
                Contract = _contract;
                GeneratePDF();
            }
        }

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);

            Init(returnedData);
        }


        void GeneratePDF()
        {
			using (PdfDocument document = new PdfDocument())
			{
				
                PdfPage page = document.Pages.Add();     //Add a page in the PDF document.

				WriteToPDF(page);

				MemoryStream m = new MemoryStream();

				document.Save(m);

                PdfDocumentStream = m;
			}
        }


        void WriteToPDF(PdfPage page)
        {
            var height = page.GetClientSize().Height;
            var width = page.GetClientSize().Width;

            // graphics element for the page
			PdfGraphics graphics = page.Graphics;
			
            // custom fonts for labels
            var Titlefont = new PdfStandardFont(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold);
            var ContractHeaderFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 14, PdfFontStyle.Bold);
            var ContractLineFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 12, PdfFontStyle.Regular);

            // Normal line format
            PdfStringFormat format = new PdfStringFormat();
            format.WordWrap = PdfWordWrapType.Word;

			// title line format
			PdfStringFormat formatTitle = new PdfStringFormat();
            formatTitle.Alignment = PdfTextAlignment.Center;

           // text elements
            PdfTextElement titleElement = new PdfTextElement(Contract.Name.ToUpper(), Titlefont, null, PdfBrushes.Black, formatTitle);
			titleElement.Draw(page, new RectangleF(0,0, page.GetClientSize().Width, 40));

			var agreements = Contract.Agreements.Where((arg) => arg.IsSelected).ToList();

            int y = 40;

            // layout each of the selected contracts
            foreach (var item in agreements)
            {
                graphics.DrawString((agreements.IndexOf(item)+1) + ".  "+ item.ContractString.Title.ToUpper(), ContractHeaderFont, PdfBrushes.Black, new PointF(20,y));
                y += 20;

                PdfTextElement textElement = new PdfTextElement(item.ContractString.Description, ContractLineFont, null, PdfBrushes.Black, format);
                textElement.Draw(page, new RectangleF(20, y, page.GetClientSize().Width-20 , 40)); 
                y += 40;
            }


            if(contract.SignImageSource!=null)
            {
                PdfBitmap image = new PdfBitmap(new MemoryStream(contract.SignImageSource) );

                graphics.DrawImage(image,new RectangleF(20,y,width/2,60));
            }


		}



	}
}
