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

        public QuotationStatus Status { get; set; } = QuotationStatus.Draft;

        public string TrainerName { get; set; }

        public string HorseShow { get; set; }

        public List<ProductQuotationModel> Products { get; set; } = new List<ProductQuotationModel>();

    }

    public enum QuotationStatus 
    {
        Quotation , Draft, Order , Payed 
    }


}
