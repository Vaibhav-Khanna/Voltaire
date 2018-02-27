using System;
using voltaire.PageModels.Base;
using System.IO;
using System.Linq;

namespace voltaire.PageModels
{
    public class EmailPDFPageModel  : BasePageModel
    {

        Stream stream;
        public Stream PdfDocumentStream { get { return stream; } set { stream = value; RaisePropertyChanged(); } }


        public override void Init(object initData)
        {
            base.Init(initData);

            var data = initData as byte[];

            if (data == null)
                return;
            
            PdfDocumentStream = new MemoryStream(data);
        }

    }
}
