using System;
using System.IO;
using System.Reflection;
using voltaire.PageModels.Base;
using Xamarin.Forms;

namespace voltaire.PageModels
{
    public class ContractPDFViewingPageModel : BasePageModel
    {

        public Command BackButton => new Command( async(obj) =>
       {
            await CoreMethods.PopPageModel();
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

        public ContractPDFViewingPageModel()
        {
			m_pdfDocumentStream = typeof(App).GetTypeInfo().Assembly.GetManifestResourceStream("outles.pdf");
        }


        public override void Init(object initData)
        {
            base.Init(initData);

        }

	}
}
