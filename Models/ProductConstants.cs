using System;
using System.Collections.Generic;

namespace voltaire.Models
{
    public static class ProductConstants
    {

        public static double TaxPercent { get; set; } = 12.5;

        public static List<string> QuantityRange { get; set; } = new List<string>() { "1","2","3","4","5","6","7","8","9","10" };

        public static List<string> ProductStatusRange { get; set; } = new List<string>() { QuotationStatus.Draft.ToString(), QuotationStatus.Quotation.ToString(), QuotationStatus.Payed.ToString(), QuotationStatus.Order.ToString() };
    }
}
