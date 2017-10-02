using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Rg.Plugins.Popup.Services;
using voltaire.Models;
using voltaire.PageModels.Base;
using voltaire.PopUps;
using voltaire.Resources;
using Xamarin.Forms;

namespace voltaire.PageModels
{
    public class QuotationDetailViewPageModel : BasePageModel
    {
        
        ProductPickerPopupModel popup_context = new ProductPickerPopupModel(); //  Popup picker model 


        public Command BackButton => new Command( async() =>
        {
            await CoreMethods?.PopPageModel();
        });

        public Command itemTapped => new Command(async (object obj) =>
		{
            var item = obj as ProductQuotationModel;
            await CoreMethods.PushPageModel<ProductDescriptionPageModel>(new Tuple<Product, ProductQuotationModel,bool>(item.Product, item, Quotation.Status == QuotationStatus.Sent ? false : true));
		});

		public Command ToolbarMenu => new Command(async () =>
		{
            var response = await CoreMethods.DisplayActionSheet(AppResources.Select, AppResources.Cancel, AppResources.DeleteQuotation , new List<string> { AppResources.InternalNotes }.ToArray());

			if (response == AppResources.InternalNotes)
			{
				// Open internal notes
            }
            else if(response == AppResources.DeleteQuotation)
            {
               // delete quotation 
            }

		});
       
        // Add product button tapped then subscribe to the selectem item changed event and take action
        public Command AddProductQuotation => new Command( async() => 
       {
            popup_context.ItemSelectedChanged += Popup_Context_ItemSelectedChanged;    // Subscribe to the event
            await PopupNavigation.PushAsync(new ProductPickerPopUp(){ BindingContext = popup_context }, true);
       });

        public Command NotesCommand => new Command( async() =>
       {
            await CoreMethods.PushPageModel<QuotationNotesPageModel>(Quotation);
       });


		public Command SignCommand => new Command(async () =>
		{
            await CoreMethods.PushPageModel<QuotationSignPageModel>(Quotation);
		});


        async void Popup_Context_ItemSelectedChanged() // When an item is selected from the popup then open product customize page
        {
            if (popup_context.SelectedItem != null)
            {
                var item = new ProductQuotationModel(popup_context.SelectedItem) { Quantity = 1 };
                OrderItemsSource.Add(item);
                quotation.Products.Add(item);

                await CoreMethods.PushPageModel<ProductDescriptionPageModel>( new Tuple<Product,ProductQuotationModel,bool>(popup_context.SelectedItem,item, Quotation.Status == QuotationStatus.Sent ? false : true));

            }
            // Unsubscribe from the event
			popup_context.ItemSelectedChanged -= Popup_Context_ItemSelectedChanged;
		}


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

                QuotationName = quotation.Name;

                HorseShow = quotation.HorseShow;

                TrainerName = quotation.TrainerName;

                TaxAmount = quotation.TaxAmount;

                ApplyTax = quotation.ApplyTax;

                SubTotal = quotation.SubTotal;

                Total = quotation.TotalAmount;
                		
                OrderItemsSource = new ObservableCollection<ProductQuotationModel>(quotation.Products);
                OrderItemsSource.CollectionChanged += OrderItemsSource_CollectionChanged;           

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

        string trainername;
        public string TrainerName
        {
            get { return trainername; }
            set 
            {
                trainername = value;
                quotation.TrainerName = trainername;
                RaisePropertyChanged();
            }
        }

        string horseshow;
        public string HorseShow
        {
            get { return horseshow; }
            set 
            {
                horseshow = value;
                quotation.HorseShow = horseshow;
                RaisePropertyChanged();
            }
        }

        string quotationname;
        public string QuotationName
        {
            get { return quotationname; }
            set 
            {
                quotationname = value;
                quotation.Name = quotationname;
                RaisePropertyChanged();
            }
        }

        double subtotal;
        public double SubTotal
        {
            get { return subtotal; }
            set 
            {
                subtotal = value;
                quotation.SubTotal = subtotal;
                RaisePropertyChanged();
            }
        }


        double total;
        public double Total
        {
            get { return total; }
            set
            {
                total = value;
                quotation.TotalAmount = total;
                RaisePropertyChanged();
            }
        }


		double taxamount;
		public double TaxAmount
		{
			get { return taxamount; }
			set
			{
				taxamount = value;
                quotation.TaxAmount = taxamount;
                OrderItemsSource_CollectionChanged(null, null);
				RaisePropertyChanged();
			}
		}

        bool applytax;
        public bool ApplyTax
        {
            get { return applytax; }
            set
            {
                applytax = value;
                quotation.ApplyTax = applytax;
                OrderItemsSource_CollectionChanged(null,null);
                RaisePropertyChanged();
            }
        }


        public void OrderItemsSource_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {

            if (OrderItemsSource == null)
                return;


            SubTotal = 0;
            Total = 0;

            foreach (var item in OrderItemsSource)
            {
                SubTotal += item.TaxFree;
            }

            if(ApplyTax)
            {
                Total = SubTotal + SubTotal * (TaxAmount/100);
            }
            else
            {
                Total = SubTotal;  
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
