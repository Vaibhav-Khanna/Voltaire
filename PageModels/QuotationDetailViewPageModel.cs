using System;
using System.Collections.ObjectModel;
using voltaire.Models;
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

        public Command AddProductQuotation => new Command( () =>
       {
            // Temporary Fix for adding products unitl choice picker is added
            var item = new ProductQuotationModel(new Product() { Name = "Saddle", Description = "This is a saddle", UnitPrice = 3000 }){ }; 
            OrderItemsSource.Add(item);
            quotation.Products.Add(item);
       });


        bool newquotation;
        public bool NewQuotation
        {
            get { return newquotation; }
            set 
            {
                newquotation = value;
                RaisePropertyChanged();
            }
        }

        Customer customer; 
        public Customer Customer
        {
            get { return customer; }
            set
            {
                customer = value;

                CustomerName = $"{customer.FirstName} {customer.LastName}";

                RaisePropertyChanged();
            }
        }

        string customername;
        public string CustomerName
        {
            get { return customername; }
            set
            {
                customername = value;
                RaisePropertyChanged();
            }
        }

        string quotationnumber;
        public string QuotationNumber
        {
            get { return quotationnumber; }
            set
            {
                quotationnumber = value;
                RaisePropertyChanged();
            }
        }

        QuotationsModel quotation;
        public QuotationsModel Quotation
        {
            get { return quotation; }
            set 
            { 
                quotation = value;

                QuotationNumber = quotation.Ref;
                		
                OrderItemsSource = new ObservableCollection<ProductQuotationModel>(quotation.Products); 

                RaisePropertyChanged();
            }
        }


        ObservableCollection<ProductQuotationModel> orderitemssource;
        public ObservableCollection<ProductQuotationModel> OrderItemsSource
        {
            get { return orderitemssource; }
            set 
            {
                orderitemssource = value;
                RaisePropertyChanged();
            }
        }


        public override void Init(object initData)
        {
            base.Init(initData);

            var _customer = initData as Tuple<Customer,bool,QuotationsModel>;  // T1 represents the customer object data , T2 is a bool which represents if its a new quotationpage

            if(_customer!=null)
            {
                NewQuotation = _customer.Item2;
                Customer = _customer.Item1;
				
                if (NewQuotation)
				{
					Quotation = new QuotationsModel() { Date = DateTime.Now, Ref = UnixTimeStamp(), Status = QuotationStatus.Quotation, TotalAmount = 0 };
					customer.Quotations.Add(Quotation);
                }
                else
                {
                    Quotation = _customer.Item3; 
                }
            }

        }

        string UnixTimeStamp()
        {
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            return unixTimestamp.ToString();
        }

    }
}
