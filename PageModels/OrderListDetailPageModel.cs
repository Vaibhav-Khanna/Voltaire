using System;
using System.Collections.ObjectModel;
using Rg.Plugins.Popup.Services;
using voltaire.Models;
using voltaire.PageModels.Base;
using voltaire.PopUps;
using voltaire.Resources;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using voltaire.Models.DataObjects;

namespace voltaire.PageModels
{
    public class OrderListDetailPageModel : BasePageModel
    {

        public Command BackButton => new Command(async () =>
       {
           await CoreMethods.PopPageModel();
       });


        public Command itemTapped => new Command(async (object obj) =>
        {
            var item = obj as ProductQuotationModel;
            await CoreMethods.PushPageModel<ProductDescriptionPageModel>(new Tuple<ProductQuotationModel, bool>(item, false));
        });


        public Command NotesCommand => new Command(async () =>
        {
            await CoreMethods.PushPageModel<PermanentNotePageModel>(Quotation);
        });


        public Command MessageCommand => new Command(async () =>
       {
           await CoreMethods.PushPageModel<MessagesPageModel>(Quotation);
       });


        public Command ToolbarMenu => new Command(async () =>
      {
          var response = await CoreMethods.DisplayActionSheet(AppResources.Select, AppResources.Cancel, null, new List<string> { AppResources.InternalNotes }.ToArray());

          if (response == AppResources.InternalNotes)
          {
               // Open internal notes
               await CoreMethods.PushPageModel<QuotationInternalNotesPageModel>(Quotation);
          }
      });


        Partner customer;
        public Partner Customer
        {
            get { return customer; }
            set
            {
                customer = value;

                CustomerName = customer.Name;

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

                QuotationNumber = AppResources.Quotation + " " + quotation.Ref + " - " + quotation.Status.ToString();

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

                var format_string = new FormattedString();

                if (quotation.DateSigned != null)
                {
                    format_string.Spans.Add(new Span() { Text = AppResources.SignedDate, FontSize = 16, FontFamily = "SanFranciscoDisplay-Medium", ForegroundColor = (Color)App.Current.Resources["GreyishBrown"] });
                    format_string.Spans.Add(new Span() { Text = ((DateTime)quotation.DateSigned).ToString("dd/MM/yyyy") + Environment.NewLine, FontSize = 16, FontFamily = "SanFranciscoDisplay-Regular", ForegroundColor = (Color)App.Current.Resources["GreyishBrown"] });
                }

                format_string.Spans.Add(new Span() { Text = AppResources.PaymentType, FontSize = 16, FontFamily = "SanFranciscoDisplay-Medium", ForegroundColor = (Color)App.Current.Resources["GreyishBrown"] });
                format_string.Spans.Add(new Span() { Text = quotation.SaleOrder.PaymentMethod + Environment.NewLine, FontSize = 16, FontFamily = "SanFranciscoDisplay-Regular", ForegroundColor = (Color)App.Current.Resources["GreyishBrown"] });

                format_string.Spans.Add(new Span() { Text = AppResources.PaymentInformation, FontSize = 16, FontFamily = "SanFranciscoDisplay-Medium", ForegroundColor = (Color)App.Current.Resources["GreyishBrown"] });
                format_string.Spans.Add(new Span() { Text = quotation.SaleOrder.PaymentNote, FontSize = 16, FontFamily = "SanFranciscoDisplay-Regular", ForegroundColor = (Color)App.Current.Resources["GreyishBrown"] });

                OrderDetails = format_string;

                RaisePropertyChanged();
            }
        }

        string currencyLogo;
        public string CurrencyLogo { get { return currencyLogo; } set { currencyLogo = value; RaisePropertyChanged(); } }

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

                RaisePropertyChanged();
            }
        }

        double _taxPercent;
        public double TaxPercent { get { return _taxPercent; } set { _taxPercent = value; RaisePropertyChanged(); } }


        ObservableCollection<DeliveryFee> deliverySource;
        public ObservableCollection<DeliveryFee> DeliverySource { get { return deliverySource; } set { deliverySource = value; RaisePropertyChanged(); } }

        DeliveryFee deliveryFee;
        public DeliveryFee DeliveryFee { get { return deliveryFee; } set { deliveryFee = value; RaisePropertyChanged(); } }
         

        FormattedString orderdetails;
        public FormattedString OrderDetails
        {
            get { return orderdetails; }
            set
            {
                orderdetails = value;
            }
        }


        public async override void Init(object initData)
        {
            base.Init(initData);

            var _customer = initData as Tuple<Partner, bool, QuotationsModel>;  // T1 represents the customer object data , T2 is a bool which represents if its a new quotationpage

            if (_customer != null)
            {
                var NewQuotation = _customer.Item2;
                Customer = _customer.Item1;

                var products = new List<ProductQuotationModel>();

                if (NewQuotation)
                {

                }
                else
                {
                    Quotation = _customer.Item3;
                   
                    var items = await StoreManager.SaleOrderLineStore.GetItemsByOrderId(quotation.SaleOrder.Id);

                    foreach (var item in items)
                    {
                        products.Add(new ProductQuotationModel(item,currencyLogo){ TaxPercent = TaxPercent });
                    }
                }

                OrderItemsSource = new ObservableCollection<ProductQuotationModel>(products);

                var deliveryData = await StoreManager.SaleOrderStore.GetDeliveryFees(false);

                if (deliveryData != null)
                {
                    DeliverySource = new ObservableCollection<DeliveryFee>(deliveryData);

                    if (DeliverySource.Where((arg) => quotation.DeliveryPrice == arg.Price).Any())
                        DeliveryFee = DeliverySource.Where((arg) => quotation.DeliveryPrice == arg.Price).First();
                }
            }
        }


    }
}
