using System;
using System.Collections.Generic;
using voltaire.Models.DataObjects;

namespace voltaire.Models
{
    public static class ProductConstants
    {

        public static void Init()
        {
            Products.Add(new Product()
            {
                Description = "This is a saddle",
                Name = "Saddle",
                Properties = new List<ProductProperty>()
                {
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Rider Name" , PropertyValue = null },
                    new ProductProperty(PropertyType.IsBoolean){ PropertyName = "Voltaire Keeper", PropertyValue = "true" },
                    new ProductProperty(PropertyType.IsBoolean){ PropertyName = "2nd Skin", PropertyValue = "false" },
                    new ProductProperty(PropertyType.IsBoolean){ PropertyName = "RBQ soft", PropertyValue = "false" },
                    new ProductProperty(PropertyType.IsPicker){ PropertyName = "Seat*", PropertyValue = null, ItemSource = SeatTypes },
                    new ProductProperty(PropertyType.IsPicker){ PropertyName = "Tree*", PropertyValue = "Mahogany", ItemSource = TreeTypes },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Rider Name" , PropertyValue = null },
                    new ProductProperty(PropertyType.IsBoolean){ PropertyName = "Voltaire Keeper", PropertyValue = "true" },
                    new ProductProperty(PropertyType.IsBoolean){ PropertyName = "2nd Skin", PropertyValue = "false" },
                    new ProductProperty(PropertyType.IsBoolean){ PropertyName = "RBQ soft", PropertyValue = "false" },
                    new ProductProperty(PropertyType.IsPicker){ PropertyName = "Seat*", PropertyValue = null, ItemSource = SeatTypes },
                    new ProductProperty(PropertyType.IsPicker){ PropertyName = "Tree*", PropertyValue = "Mahogany", ItemSource = TreeTypes },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Rider Name" , PropertyValue = null },
                    new ProductProperty(PropertyType.IsBoolean){ PropertyName = "Voltaire Keeper", PropertyValue = "true" },
                    new ProductProperty(PropertyType.IsBoolean){ PropertyName = "2nd Skin", PropertyValue = "false" },
                    new ProductProperty(PropertyType.IsBoolean){ PropertyName = "RBQ soft", PropertyValue = "false" },
                    new ProductProperty(PropertyType.IsPicker){ PropertyName = "Seat*", PropertyValue = null, ItemSource = SeatTypes },
                    new ProductProperty(PropertyType.IsPicker){ PropertyName = "Tree*", PropertyValue = "Mahogany", ItemSource = TreeTypes }

                }
            });
            Products.Add(new Product() { Description = "This is a accessories", Name = "Accessories" });
            Products.Add(new Product()
            {
                Description = "This is a service",
                Name = "Service",
                Properties = new List<ProductProperty>()
                {
                    new ProductProperty(PropertyType.IsBoolean){ PropertyName = "RBQ soft", PropertyValue = "false" },
                    new ProductProperty(PropertyType.IsPicker){ PropertyName = "Seat*", PropertyValue = null, ItemSource = SeatTypes },
                    new ProductProperty(PropertyType.IsPicker){ PropertyName = "Tree*", PropertyValue = "Mahogany", ItemSource = TreeTypes },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Rider Name" , PropertyValue = null },
                    new ProductProperty(PropertyType.IsBoolean){ PropertyName = "Voltaire Keeper", PropertyValue = "true" },
                    new ProductProperty(PropertyType.IsBoolean){ PropertyName = "2nd Skin", PropertyValue = "false" },
                    new ProductProperty(PropertyType.IsBoolean){ PropertyName = "RBQ soft", PropertyValue = "false" },
                    new ProductProperty(PropertyType.IsPicker){ PropertyName = "Seat*", PropertyValue = null, ItemSource = SeatTypes },
                    new ProductProperty(PropertyType.IsPicker){ PropertyName = "Tree*", PropertyValue = "Mahogany", ItemSource = TreeTypes }
                }
            });
            Products.Add(new Product()
            {
                Description = "This is  others",
                Name = "Others",
                Properties = new List<ProductProperty>()
                {
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Name", PropertyValue = null },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Price", PropertyValue = null},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Description", PropertyValue = null}
                }

            });
            Products.Add(new Product() { Description = "This is a tradein", Name = "TradeIn" });
            Products.Add(new Product() { Description = "This is a discount", Name = "Discount" });

            Agreements.Add(new Agreement() { Title = "1) You will have to obey to all the rules and regulations mentioned by the company." });
            Agreements.Add(new Agreement() { Title = "2) You will have to obey to all the rules and regulations mentioned by the company." });
            Agreements.Add(new Agreement() { Title = "3) You will have to obey to all the rules and regulations mentioned by the company." });
            Agreements.Add(new Agreement() { Title = "4) You will have to obey to all the rules and regulations mentioned by the company." });
            Agreements.Add(new Agreement() { Title = "5) You will have to obey to all the rules and regulations mentioned by the company." });
            Agreements.Add(new Agreement() { Title = "6) You will have to obey to all the rules and regulations mentioned by the company." });
            Agreements.Add(new Agreement() { Title = "7) You will have to obey to all the rules and regulations mentioned by the company." });
            Agreements.Add(new Agreement() { Title = "8) You will have to obey to all the rules and regulations mentioned by the company." });
            Agreements.Add(new Agreement() { Title = "9) You will have to obey to all the rules and regulations mentioned by the company." });
            Agreements.Add(new Agreement() { Title = "10) You will have to obey to all the rules and regulations mentioned by the company." });
            Agreements.Add(new Agreement() { Title = "11) You will have to obey to all the rules and regulations mentioned by the company." });
            Agreements.Add(new Agreement() { Title = "12) You will have to obey to all the rules and regulations mentioned by the company." });
            Agreements.Add(new Agreement() { Title = "13) You will have to obey to all the rules and regulations mentioned by the company." });


        }

        public static double TaxPercent { get; set; } = 12.5;

        public static List<Product> Products { get; set; } = new List<Product>() { };

        public static List<Agreement> Agreements { get; set; } = new List<Agreement>();

        public static List<string> SeatTypes { get; set; } = new List<string>() { "Big", "Small", "Medium", "Extra Large" };

        public static List<string> TreeTypes { get; set; } = new List<string>() { "Oak", "Mahogany", "Pine", "Spruce" };

        public static List<string> QuantityRange { get; set; } = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };

        public static List<string> TagList { get; set; } = new List<string>() { "Pro", "Nice", "Late Payment", "Behaviour" };

        public static List<string> ProductStatusRange { get; set; } = new List<string>() { QuotationStatus.Draft.ToString(), QuotationStatus.Quotation.ToString(), QuotationStatus.Payed.ToString(), QuotationStatus.Order.ToString() };
    }
}
