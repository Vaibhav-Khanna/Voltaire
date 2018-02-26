using System;
using System.Collections.Generic;
using System.Linq;
using voltaire.Models.DataObjects;
using voltaire.PageModels;
using Xamarin.Forms;

namespace voltaire.Models
{
    public class QuotationsModel
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

        string currencyLogo;
        public string CurrencyLogo { get { return currencyLogo; } set { currencyLogo = value; } }

        string name;
        public string Name { get { return name; } set { name = value; SaleOrder.Name = value; } }

        string reference;
        public string Ref { get { return reference; } set{ reference = value; SaleOrder.ClientOrderRef = value; } }

        DateTime date;
        public DateTime Date { get { return date; } set { date = value; SaleOrder.DateOrder = value; } }

        double totalAmount;
        public double TotalAmount { get { return totalAmount; } set { totalAmount = value; SaleOrder.AmountTotal = Convert.ToInt64(value); } }

        double subTotal;
        public double SubTotal { get { return subTotal; } set { subTotal = value; SaleOrder.AmountUntaxed = Convert.ToInt64(value); } }

        bool applyTax;
        public bool ApplyTax { get { return applyTax; } set{ applyTax = value; } }

        double taxAmount;
        public double TaxAmount { get { return taxAmount; } set { taxAmount = value; SaleOrder.AmountTax = Convert.ToInt64(value); } }

        string permanote;
        public string PermanentNote { get { return permanote; } set { permanote = value; SaleOrder.Note = value; } }

        public List<Message> Messages { get; set; } = new List<Message>();

        string status;
        public string Status { get { return status; } set { status = value; SaleOrder.State = value; } }

        string trainerName;
        public string TrainerName { get { return trainerName; } set { trainerName = value; SaleOrder.TrainerName = trainerName; } }

        string horseShow;
        public string HorseShow { get { return horseShow; } set { horseShow = value; SaleOrder.HorseShow = horseShow; } }

        public List<ProductQuotationModel> Products { get; set; } = new List<ProductQuotationModel>();

        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.None;

        public bool IsConditionsAgree { get; set; }

        public string PaymentNotes { get; set; }

        public bool IsSignedValidated { get; set; }

        public byte[] SignedImage { get; set; }

        public DateTime? DateSigned { get; set; }

        public List<string> TermsConditions { get; set; } = new List<string> { "20% cancellation fee on any custom order", "2 weeks return policy on any custom item" };

    }

    public enum QuotationStatus
    {
        cancel, draft, sale, done, sent
    }

    public enum PaymentMethod
    {
        None, CreditCard, DebitCard, Cash
    }


}
