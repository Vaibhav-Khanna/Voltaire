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
            Products.Add(new Product()
            {
                Description = "This is a accessories",
                Name = "Accessories",
                Properties = new List<ProductProperty>()
                {
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Category" , PropertyValue = null },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Name", PropertyValue = null },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Sale price", PropertyValue = null },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Note", PropertyValue = null },
                }
            }); //Normally Note is an Editor

            Products.Add(new Product()
            {
                Description = "This is a service",
                Name = "Service",
                Properties = new List<ProductProperty>()
                {
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Serail number", PropertyValue = null },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Brand", PropertyValue = null},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Model", PropertyValue = null},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Seat size", PropertyValue = null},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Flap size", PropertyValue = null},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Leather", PropertyValue = null},
                    new ProductProperty(PropertyType.IsPicker){ PropertyName = "Color", PropertyValue = null, ItemSource = ColorTypes },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Category", PropertyValue = null},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Comments", PropertyValue = null},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Sale price", PropertyValue = null},
                }
            }); //Normally Comments is an editor

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

            }); //Normally Description is an editor

            Products.Add(new Product()
            {
                Description = "This is a tradein",
                Name = "TradeIn",
                Properties = new List<ProductProperty>()
                {
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Serail number", PropertyValue = null },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Brand", PropertyValue = null},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Model", PropertyValue = null},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Size", PropertyValue = null},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Flap", PropertyValue = null},
                    new ProductProperty(PropertyType.IsPicker){ PropertyName = "Color", PropertyValue = null, ItemSource = ColorTypes },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Leather", PropertyValue = null},
                    new ProductProperty(PropertyType.IsBoolean){ PropertyName = "Blocks", PropertyValue = null},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Other", PropertyValue = null},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Amount", PropertyValue = null},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Note", PropertyValue = null},
                }

            }); //Normally N is an editor

            Products.Add(new Product()
            {
                Description = "This is a discount",
                Name = "Discount",
                Properties = new List<ProductProperty>()
                {
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Name", PropertyValue = null },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Sale Price", PropertyValue = null},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Note", PropertyValue = null}
                }
            }); //Normally Note is an editor








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

        public static List<string> ColorTypes { get; set; } = new List<string>() { "Light brown", "Chocolate", "Black" };

        public static List<string> SeatTypes { get; set; } = new List<string>() { "Big", "Small", "Medium", "Extra Large" };

        public static List<string> TreeTypes { get; set; } = new List<string>() { "Oak", "Mahogany", "Pine", "Spruce" };

        public static List<string> QuantityRange { get; set; } = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };

        public static List<string> TagList { get; set; } = new List<string>() { "Pro", "Nice", "Late Payment", "Behaviour" };

        public static List<string> ProductStatusRange { get; set; } = new List<string>() { QuotationStatus.sent.ToString(), QuotationStatus.draft.ToString(), QuotationStatus.cancel.ToString(), QuotationStatus.done.ToString(), QuotationStatus.sale.ToString() };
    }
}
