using System;
using voltaire.PageModels.Base;
using System.Collections.Generic;
using voltaire.Models;

namespace voltaire.PageModels
{
    public class MapMainPageModel : BasePageModel
    {
        public MapMainPageModel()
        {
            
        }

        List<CustomerAddressLocation> customeraddresses = new List<CustomerAddressLocation>();
        public List<CustomerAddressLocation> CustomerAddresses 
        {
            get { return customeraddresses; }
            set
            {
                customeraddresses = value;
                RaisePropertyChanged();
            }
        }


        public override void Init(object initData)
        {
            base.Init(initData);

            // Mock Data
            CustomerAddresses = new List<CustomerAddressLocation>(){ new CustomerAddressLocation(){ Title = "My House", Address = "Long Island", Latitude = 33.3453, Longitude = 77.2312 } };

        }

    }
}
