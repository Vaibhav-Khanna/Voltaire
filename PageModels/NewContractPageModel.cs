using System;
using FreshMvvm;
using voltaire.Models;
using Xamarin.Forms;


namespace voltaire.PageModels
{
    public class NewContractPageModel : FreshBasePageModel
    {
        Customer customer;
        public Customer Customer 
        {
            get { return customer; }
            set
            {
                customer = value;

                NewContract = $"New contract for {customer.FirstName} {customer.LastName}";

                RaisePropertyChanged();
            }
        }

        public Command BackButton => new Command( async(obj) =>
       {
            await CoreMethods.PopPageModel();
       });


        string newcontract;
        public string NewContract 
        {
            get { return newcontract; }
            set
            {
                newcontract = value;
                RaisePropertyChanged();
            }
        }


        public override void Init(object initData)
        {
            base.Init(initData);

            var context = initData as Customer;

            Customer = context;
        }
    }
}
