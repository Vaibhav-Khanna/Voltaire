using System;
using Xamarin.Forms;

namespace voltaire.Models
{
    public class QuotationsModel
    {
        public QuotationsModel()
        {
            
        }

        public Color BackColor { get; set; }

        public string Name { get; set; }

        public string Ref { get; set; }

        public DateTime Date { get; set; }

        public double TotalAmount { get; set; }

        public QuotationStatus Status { get; set; }

    }

    public enum QuotationStatus 
    {
        Quotation , Draft, Order , Payed 
    }


}
