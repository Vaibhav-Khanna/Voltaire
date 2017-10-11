using System;
using System.Collections.Generic;
using voltaire.PageModels;
using Xamarin.Forms;

namespace voltaire.Models
{
    public class QuotationsModel
    {
        
        public Color BackColor { get; set; }

        public string Name { get; set; }

        public string Ref { get; set; }

        public DateTime Date { get; set; }

        public double TotalAmount { get; set; }

        public double SubTotal { get; set; }

        public bool ApplyTax { get; set; }

        public double TaxAmount { get; set; }

        public List<Note> InternalNotes { get; set; } = new List<Note>();

        public List<Message> Messages { get; set; } = new List<Message>();

        public string PermanentNote { get; set; }

        public QuotationStatus Status { get; set; } = QuotationStatus.Draft;

        public string TrainerName { get; set; }

        public string HorseShow { get; set; }

        public List<ProductQuotationModel> Products { get; set; } = new List<ProductQuotationModel>();

        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.None;

        public bool IsConditionsAgree { get; set; }

        public string PaymentNotes { get; set; }

        public bool IsSignedValidated { get; set; }

        public byte[] SignedImage { get; set; }

        public DateTime? DateSigned { get; set; }

        public List<string> TermsConditions { get; set; } = new List<string> { "20% cancellation fee on any custom order" , "2 weeks return policy on any custom item" };

    }

    public enum QuotationStatus 
    {
        Quotation , Draft, Order , Payed , Sent
    }

    public enum PaymentMethod
    {
        None, CreditCard, DebitCard, Cash
    }


}
