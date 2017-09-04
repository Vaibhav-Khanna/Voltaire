using System;
using voltaire.Models;
using voltaire.PageModels.Base;
using voltaire.Resources;
using Xamarin.Forms;

namespace voltaire.PageModels
{
    public class ReportsDetailPageModel : BasePageModel
    {
        public SalesmanModel SalesmanModelInit { get; set; }
        private Salesman _salesman;
        public string name { get; set; }

        public Command tap_Back => new Command(async () =>
        {

            await CoreMethods.PopPageModel(null, false, false);
            ReleaseResources();

        });

        public override void Init(object initData)
        {

            base.Init(initData);

            if (initData != null)
                _salesman = (Salesman)initData;

            SalesmanModelInit = new SalesmanModel(_salesman);
            name = AppResources.Report + " " + SalesmanModelInit.Name;

        }
    }
}
