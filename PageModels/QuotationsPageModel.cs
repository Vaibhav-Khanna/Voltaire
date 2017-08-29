using System;
using System.Collections.Generic;
using voltaire.Models;
using voltaire.PageModels.Base;
using Xamarin.Forms;

namespace voltaire.PageModels
{
    public class QuotationsPageModel : BasePageModel
    {

		Customer customer;
		public Customer Customer
		{
			get { return customer; }
			set
			{
				customer = value;

                filter = "All";

                RaisePropertyChanged();
                RaisePropertyChanged(nameof(Filter));
			}
		}

        public List<string> FilterTypes { get; set; } = new List<string>() { "All", "none", "Pro" };


        string filter { get; set; }
        public string Filter 
        {
            get { return filter; }
            set 
            {
                filter = value; 
                RaisePropertyChanged();
            }
        }


        public Command FilterTap => new Command(() =>
       {

       });



        public override void Init(object initData)
        {
            base.Init(initData);

			var context = initData as Customer;

			if (context == null)
				return;

            Customer = context;

        }


    }
}
