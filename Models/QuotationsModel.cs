using System;
using System.Collections.Generic;
using System.Linq;
using voltaire.Models.DataObjects;
using voltaire.PageModels;
using Xamarin.Forms;

namespace voltaire.Models
{
    public class QuotationsModel : BaseModel
    {

        public QuotationsModel(SaleOrder model)
        {
            if (model != null)
            {
                SaleOrder = model;
                Name = model.Name;
                Ref = model.ClientOrderRef;


                TaxAmount = model.AmountTax;

                TotalAmount = model.AmountTotal;
                SubTotal = model.AmountUntaxed;
                ApplyTax = (model.AmountTax) <= 0 ? false : true;

                TaxPercent = model.TaxPercent;
                DeliveryPrice = model.DeliveryPrice;

                PermanentNote = model.Note;
                Date = model.DateOrder;
                Status = model.State;
                HorseShow = model.HorseShow;
                TrainerName = model.TrainerName;

                if (ProductConstants.CurrencyValues.Any() && ProductConstants.CurrencyValues.Where((arg) => arg.Key == model.CurrencyId).Any())
                    CurrencyLogo = ProductConstants.CurrencyValues.Where((arg) => arg.Key == model.CurrencyId)?.First().Value;
                else
                    CurrencyLogo = "*";
            }
        }

        public SaleOrder SaleOrder { get; set; }

        public Color BackColor { get; set; }

        double _deliveryPrice;
        public double DeliveryPrice { get { return _deliveryPrice; } set { _deliveryPrice = value; SaleOrder.DeliveryPrice = value; RaisePropertyChanged(); } }

        double _taxpercent;
        public double TaxPercent { get { return _taxpercent; } set { _taxpercent = value; SaleOrder.TaxPercent = value; RaisePropertyChanged(); } }

        string currencyLogo;
        public string CurrencyLogo { get { return currencyLogo; } set { currencyLogo = value; RaisePropertyChanged(); } }

        string name;
        public string Name { get { return name; } set { name = value; SaleOrder.Name = value; RaisePropertyChanged(); } }

        string reference;
        public string Ref { get { return reference; } set{ reference = value; SaleOrder.ClientOrderRef = value; RaisePropertyChanged(); } }

        DateTime date;
        public DateTime Date { get { return date; } set { date = value; SaleOrder.DateOrder = value; SaleOrder.CreateDate = value; RaisePropertyChanged();  } }

        double totalAmount;
        public double TotalAmount { get { return totalAmount; } set { totalAmount = value;  SaleOrder.AmountTotal = value; RaisePropertyChanged(); } }

        double subTotal;
        public double SubTotal { get { return subTotal; } set { subTotal = value; SaleOrder.AmountUntaxed = value; RaisePropertyChanged(); } }

        bool applyTax;
        public bool ApplyTax { get { return applyTax; } set{ applyTax = value; RaisePropertyChanged(); } }

        double taxAmount;
        public double TaxAmount { get { return taxAmount; } set { taxAmount = value; SaleOrder.AmountTax = value; RaisePropertyChanged(); } }

        string permanote;
        public string PermanentNote { get { return permanote; } set { permanote = value; SaleOrder.Note = value; RaisePropertyChanged(); } }

        public List<Message> Messages { get; set; } = new List<Message>();

        string status;
        public string Status { get { return status; } set { status = value; SaleOrder.State = value; RaisePropertyChanged(); } }

        string trainerName;
        public string TrainerName { get { return trainerName; } set { trainerName = value; SaleOrder.TrainerName = trainerName; RaisePropertyChanged(); } }

        string horseShow;
        public string HorseShow { get { return horseShow; } set { horseShow = value; SaleOrder.HorseShow = horseShow; RaisePropertyChanged(); } }

        public List<ProductQuotationModel> Products { get; set; } = new List<ProductQuotationModel>();

        PaymentMethod _method = PaymentMethod.None;
        public PaymentMethod PaymentMethod { get { return _method; } set { _method = value; SaleOrder.PaymentMethod = value.ToString(); RaisePropertyChanged(); } } 

        public bool IsConditionsAgree { get; set; }

        string notes;
        public string PaymentNotes { get { return notes; } set { notes = value; SaleOrder.PaymentNote = notes; RaisePropertyChanged(); } }

        public bool IsSignedValidated { get; set; }

        public byte[] SignedImage { get; set; }

        public DateTime? DateSigned { get; set; }

        public List<string> TermsConditions { get; set; } = new List<string> { Resources.AppResources.Terms1, Resources.AppResources.Terms2 };
    }

    public enum QuotationStatus
    {
        cancel, draft, sale, done, sent
    }

    public enum ProductStatus
    {
        Order, Delivered, Received, ToBeReceived
    }

    public enum PaymentMethod
    {
        None, CreditCard, WireTransfer, Cash, Cheque
    }

}
