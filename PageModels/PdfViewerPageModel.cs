using System;
using System.IO;
using voltaire.PageModels.Base;
using Xamarin.Forms;

namespace voltaire.PageModels
{
    public class PdfViewerPageModel : BasePageModel
    {
        string _title;
        public string Title { get { return _title; } set { _title = value; RaisePropertyChanged(); } }

        Stream stream;
        public Stream PdfDocumentStream { get { return stream; } set { stream = value; RaisePropertyChanged(); } }

        public Command BackButton => new Command(async() =>
        {
            await CoreMethods.PopPageModel();
        });

        public override void Init(object initData)
        {
            base.Init(initData);

            if(initData is Tuple<string,byte[]>)
            {
                Title = ((Tuple<string, byte[]>)initData).Item1;
                PdfDocumentStream = new MemoryStream(((Tuple<string, byte[]>)initData).Item2);
            }
        }
    }
}
