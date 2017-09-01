using System;
using System.Collections.Generic;

namespace voltaire.Models
{
    public static class ProductConstants
    {

        public static void Init()
        {
            Products.Add(new Product(){ Description = "This is a saddle", Name = "Saddle", UnitPrice = 3000 });
			Products.Add(new Product() { Description = "This is a service", Name = "Service", UnitPrice = 400 });
			Products.Add(new Product() { Description = "This is a discount", Name = "Discount", UnitPrice = 550 });
			Products.Add(new Product() { Description = "This is a seat", Name = "Seat", UnitPrice = 15000 });
			Products.Add(new Product() { Description = "This is a tradein", Name = "TradeIn", UnitPrice = 200 });
			Products.Add(new Product() { Description = "This is a horse", Name = "Horse", UnitPrice = 10 });
		}

        public static double TaxPercent { get; set; } = 12.5;

        public static List<Product> Products { get; set; } = new List<Product>() { };

        public static List<string> QuantityRange { get; set; } = new List<string>() { "1","2","3","4","5","6","7","8","9","10" };

        public static List<string> ProductStatusRange { get; set; } = new List<string>() { QuotationStatus.Draft.ToString(), QuotationStatus.Quotation.ToString(), QuotationStatus.Payed.ToString(), QuotationStatus.Order.ToString() };
    }
}
