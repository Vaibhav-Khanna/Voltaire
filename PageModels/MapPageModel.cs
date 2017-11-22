using System;
using voltaire.Models;
using voltaire.PageModels.Base;

namespace voltaire.PageModels
{
    public class MapPageModel : BasePageModel
    {


		Partner customer;

        public Partner Customer 
        {
            get { return customer; }
            set 
            {
                customer = value;
            }
        }


        public MapPageModel()
        {
            
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            var context = initData as Partner;

            if (context == null)
                return;

        }

    }
}
