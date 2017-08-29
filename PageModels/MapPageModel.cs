using System;
using voltaire.Models;
using voltaire.PageModels.Base;

namespace voltaire.PageModels
{
    public class MapPageModel : BasePageModel
    {


		Customer customer;

        public Customer Customer 
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

            var context = initData as Customer;

            if (context == null)
                return;

        }

    }
}
