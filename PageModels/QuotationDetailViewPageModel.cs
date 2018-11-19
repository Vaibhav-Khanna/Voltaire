using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Acr.UserDialogs;
using Rg.Plugins.Popup.Services;
using voltaire.Models;
using voltaire.Models.DataObjects;
using voltaire.PageModels.Base;
using voltaire.PopUps;
using voltaire.Resources;
using Xamarin.Forms;
using System.Linq;
using voltaire.Helpers;

namespace voltaire.PageModels
{
    public class QuotationDetailViewPageModel : BasePageModel
    {

        ProductPickerPopupModel popup_context = new ProductPickerPopupModel(); //  Popup picker model


        public Command BackButton => new Command(async () =>
       {
           UserDialogs.Instance.ShowLoading($"{AppResources.JustAMoment}...");
          
           await StoreManager.SaleOrderStore.UpdateAsync(Quotation.SaleOrder);

           foreach (var item in OrderItemsSource)
           {
               await StoreManager.SaleOrderLineStore.UpdateAsync(item.Product);
           }

           foreach (var item in OrderItemsSource)
           {
               item.PropertyChanged -= Item_PropertyChanged;
           }

           UserDialogs.Instance.HideLoading();
             
            await CoreMethods?.PopPageModel(NewQuotation ? Quotation : null);
       });


        public Command DeleteItemCommand => new Command(async (obj) =>
        {
            if (obj != null && obj is ProductQuotationModel)
            {
                var item = (ProductQuotationModel)obj;

                if (OrderItemsSource.Contains(item))
                {
                    Dialog.ShowLoading("");
                   
                    OrderItemsSource.Remove(item);
                    quotation.Products.Remove(item);
                    await StoreManager.SaleOrderLineStore.RemoveAsync(item.Product);

                    Dialog.HideLoading();
                }
            }
        });


        public Command itemTapped => new Command(async (object obj) =>
        {
            var item = obj as ProductQuotationModel;
            await CoreMethods.PushPageModel<ProductDescriptionPageModel>(new Tuple<ProductQuotationModel, bool>(item,CanEdit));
        });

        public Command ToolbarMenu => new Command(async () =>
        {

            string delete_text;

            // check the status before passing on deletion option to user
            if (CanEdit)
                delete_text = AppResources.DeleteQuotation;
            else
                delete_text = null;

            var response = await CoreMethods.DisplayActionSheet(AppResources.Select, AppResources.Cancel, delete_text, new List<string> { AppResources.InternalNotes }.ToArray());

            if (response == AppResources.InternalNotes)
            {
                // Open internal notes
                await CoreMethods.PushPageModel<QuotationInternalNotesPageModel>(Quotation);

            }
            else if (response == AppResources.DeleteQuotation)
            {
                // delete quotation 
                UserDialogs.Instance.ShowLoading($"{AppResources.Deleting}...");
                await StoreManager.SaleOrderStore.RemoveAsync(Quotation.SaleOrder);
                UserDialogs.Instance.HideLoading();
                await CoreMethods?.PopPageModel(Quotation);
            }

        });

        // Add product button tapped then subscribe to the selectem item changed event and take action
        public Command AddProductQuotation => new Command(async () =>
      {
          popup_context.ItemSelectedChanged += Popup_Context_ItemSelectedChanged;    // Subscribe to the event
           await PopupNavigation.PushAsync(new ProductPickerPopUp() { BindingContext = popup_context }, true);
      });

        public Command NotesCommand => new Command(async () =>
      {
          await CoreMethods.PushPageModel<PermanentNotePageModel>(Quotation);
      });


        public Command SignCommand => new Command(async () =>
        {
            await CoreMethods.PushPageModel<QuotationSignPageModel>(new Tuple<QuotationsModel,Partner,List<ProductQuotationModel>>(Quotation,Customer,OrderItemsSource.ToList()));
        });

        public Command EmailCommand => new Command(async (obj) =>
       {
           // Generate or check PDF file
            var resp = await CoreMethods.DisplayAlert(AppResources.Alert, AppResources.WantToSendNow, AppResources.Yes, AppResources.NotNow);

           if (!resp)
               return;

            Dialog.ShowLoading($"{AppResources.SendingMail}...");

           var vendorItem = await StoreManager.DocumentStore.GetItemByEmployeeId(quotation.SaleOrder.Id, "vendor");

           var customeritem = await StoreManager.DocumentStore.GetItemByEmployeeId(quotation.SaleOrder.Id, "customer");

            var invoice = new InvoiceGenerate();

            var customerPDF = invoice.CreatePdfFile(Quotation, OrderItemsSource.ToList(), Customer, null, false);

           var vendorPDF = invoice.CreatePdfFile(Quotation, OrderItemsSource.ToList(), Customer, null, true);

           if (vendorItem == null) // nothing exists in storage
           {    
                var document = new Document() { Path = quotation.SaleOrder.Id + '/' + "customer.pdf", Name = quotation.SaleOrder.Id + ".pdf", InternalName = "customer", ReferenceKind = "saleOrder", ReferenceId = quotation.SaleOrder.Id, MimeType = "application/pdf" };

               await StoreManager.DocumentStore.InsertImage(customerPDF, document);

                var documentVendor = new Document() { Path = quotation.SaleOrder.Id + '/' + "vendor.pdf", Name = quotation.SaleOrder.Id + ".pdf", InternalName = "vendor", ReferenceKind = "saleOrder", ReferenceId = quotation.SaleOrder.Id, MimeType = "application/pdf" };

               await StoreManager.DocumentStore.InsertImage(vendorPDF, documentVendor);
           }
           else
           {
               await PclStorage.SaveFileLocal(vendorPDF, vendorItem.Id);

               await PclStorage.SaveFileLocal(customerPDF, customeritem.Id);

               vendorItem.ToUpload = true;
               customeritem.ToUpload = true;

               await StoreManager.DocumentStore.UpdateAsync(customeritem);
               await StoreManager.DocumentStore.UpdateAsync(vendorItem);

               await StoreManager.DocumentStore.OfflineUploadSync();

           }
         
           quotation.SaleOrder.ToSend = true;

           await StoreManager.SaleOrderStore.UpdateAsync(quotation.SaleOrder);

            Dialog.HideLoading();

            await CoreMethods.DisplayAlert(AppResources.Alert,AppResources.DocumentSent, AppResources.Ok);

       });


        async void Popup_Context_ItemSelectedChanged() // When an item is selected from the popup then open product customize page
        {
            if ( popup_context.SelectedItem != null)
            {
                var item = new ProductQuotationModel(new SaleOrderLine() { OrderId = Quotation.SaleOrder.Id, CurrencyId = Quotation.SaleOrder.CurrencyId, ProductKind = popup_context.SelectedItem.ProductKind.ToString(), TaxId = 2, PriceUnit = popup_context.SelectedItem.UnitPrice },CurrencyLogo) { Quantity = 1 };               
                              
                item.Product.ConfigurationDetail = Newtonsoft.Json.JsonConvert.SerializeObject(popup_context.SelectedItem.Properties);

                item.PropertyChanged += Item_PropertyChanged;

                OrderItemsSource.Add(item);

                quotation.Products.Add(item);

                await CoreMethods.PushPageModel<ProductDescriptionPageModel>(new Tuple<ProductQuotationModel, bool>( item,CanEdit));

                await StoreManager.SaleOrderLineStore.InsertAsync(item.Product);
            } 
           
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

        Partner customer;
        public Partner Customer
        {
            get { return customer; }
            set
            {
                customer = value;

                CustomerName = $"{customer.Name}";

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

                TaxPercent = quotation.TaxPercent;

                if (ProductConstants.CurrencyValues.Any() && ProductConstants.CurrencyValues.Where((arg) => arg.Key == quotation.SaleOrder.CurrencyId).Any())
                    CurrencyLogo = ProductConstants.CurrencyValues.Where((arg) => arg.Key == quotation.SaleOrder.CurrencyId)?.First().Value;
                else
                    CurrencyLogo = "*"; //"€";

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

        string currencyLogo;
        public string CurrencyLogo { get { return currencyLogo; } set { currencyLogo = value; RaisePropertyChanged(); } }


        double taxamount;
        public double TaxAmount
        {
            get { return taxamount; }
            set
            {
                taxamount = value;
               
                quotation.TaxAmount = taxamount;              
             
                RaisePropertyChanged();
            }
        }

        double _taxPercent;
        public double TaxPercent
        {
            get { return _taxPercent; }
            set
            {
                _taxPercent = value;
                OrderItemsSource_CollectionChanged(null, null);

                if (quotation != null)
                {
                    quotation.TaxPercent = value;

                    ApplyTax = Convert.ToInt32(value) != 0;
                }

                RaisePropertyChanged();
            }
        }


        ObservableCollection<DeliveryFee> deliverySource;
        public ObservableCollection<DeliveryFee> DeliverySource { get { return deliverySource; } set { deliverySource = value; RaisePropertyChanged(); } }

        DeliveryFee deliveryFee;
        public DeliveryFee DeliveryFee { get { return deliveryFee; } set { deliveryFee = value; quotation.DeliveryPrice = value.Price; OrderItemsSource_CollectionChanged(null, null); RaisePropertyChanged(); } }


        bool applytax;
        public bool ApplyTax
        {
            get { return applytax; }
            set
            {
                applytax = value;

                if(quotation!=null)
                if (quotation.ApplyTax != value)
                {
                    quotation.ApplyTax = applytax;

                    OrderItemsSource_CollectionChanged(null, null);
                }

                RaisePropertyChanged();
            }
        }

        bool canedit;
        public bool CanEdit
        {
            get { return canedit; }
            set
            {
                canedit = value;
                RaisePropertyChanged();
            }
        }


        public void OrderItemsSource_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {

            if (OrderItemsSource == null)
                return;

            SubTotal = 0;
            Total = 0;
            TaxAmount = 0;

            foreach (var item in OrderItemsSource)
            {
                item.TaxPercent = item.ProductKind == ProductKind.discount ? 0 : TaxPercent;
                item.IsTaxApply = true;
            }

            foreach (var item in OrderItemsSource)
            {
                SubTotal += item.TaxFree;

                TaxAmount += (Convert.ToInt32(item.UnitPrice) * item.Quantity) - item.TaxFree;
            }

            //add delivery fee
            if (DeliveryFee != null)
            {
                SubTotal += deliveryFee.Price;

                TaxAmount += deliveryFee.Price * (double)TaxPercent/100;
            }

            Total = SubTotal + TaxAmount; 
        }


        public async override void Init(object initData)
        {
            base.Init(initData);
          

            var _customer = initData as Tuple<Partner, bool, QuotationsModel>; 

            if (_customer != null)
            {
                NewQuotation = _customer.Item2;
                Customer = _customer.Item1;
                var products = new List<ProductQuotationModel>();

                var currUser = await StoreManager.UserStore.GetCurrentUserAsync();

                if (currUser == null)
                    return;

                if (NewQuotation)
                {
                    var saleOrder = new SaleOrder(){ PartnerId = Customer.ExternalId, UserId = currUser.ExternalId, CurrencyId = ProductConstants.CurrencyValues.Keys.First() };

                    Quotation = new QuotationsModel( saleOrder ) { Date = DateTime.UtcNow, Ref = Customer.ExternalId + "-" + UnixTimeStamp(), Status = QuotationStatus.draft.ToString() , TotalAmount = 0 };
                    InsertNewQuotation(saleOrder);
                    customer.Quotations.Add(Quotation);
                }
                else
                {
                    Quotation = _customer.Item3;
                  
                    var items = await StoreManager.SaleOrderLineStore.GetItemsByOrderId(quotation.SaleOrder.Id);
                                      
                    foreach (var item in items)
                    {
                        products.Add(new ProductQuotationModel(item,CurrencyLogo));
                    }
                }

                OrderItemsSource = new ObservableCollection<ProductQuotationModel>(products);
                OrderItemsSource.CollectionChanged += OrderItemsSource_CollectionChanged;

                foreach (var item in OrderItemsSource)
                {
                    item.PropertyChanged += Item_PropertyChanged;
                }

                var deliveryData = await StoreManager.SaleOrderStore.GetDeliveryFees(false);

                if(deliveryData!=null)
                {
                    DeliverySource = new ObservableCollection<DeliveryFee>(deliveryData);

                    if (DeliverySource.Where((arg) => quotation.DeliveryPrice == arg.Price).Any())
                        DeliveryFee = DeliverySource.Where((arg) => quotation.DeliveryPrice == arg.Price).First();
                }

            }
        }

        void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "UnitPrice" || e.PropertyName == "Quantity")
            {
               OrderItemsSource_CollectionChanged(null, null);
            }
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            CanEdit = Quotation.Status == QuotationStatus.sale.ToString() || Quotation.Status == QuotationStatus.done.ToString() ? false : true;

            if (!CanEdit)
            {
                QuotationNumber = AppResources.Quotation + " " + quotation.Ref + " - " + quotation.Status.ToString();
            }

            if(OrderItemsSource!=null)
            foreach (var item in OrderItemsSource)
            {
                item.CanEdit = CanEdit;
            }
        }

        string UnixTimeStamp()
        {
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            return unixTimestamp.ToString();
        }

        async void InsertNewQuotation(SaleOrder order)
        {
            await StoreManager.SaleOrderStore.InsertAsync(order);
        }

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);

            if(returnedData is ProductQuotationModel)
            {
                DeleteItemCommand.Execute(returnedData);
            }
        }
    }
}
