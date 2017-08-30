using System;
using voltaire.PageModels.Base;
using Xamarin.Forms;

namespace voltaire.PageModels
{
    public class QuotationDetailViewPageModel : BasePageModel
    {

        public Command BackButton => new Command( async() =>
        {
            await CoreMethods?.PopPageModel();
        });


        public override void Init(object initData)
        {
            base.Init(initData);
        }
    }
}
