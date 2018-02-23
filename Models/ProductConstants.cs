using System;
using System.Collections.Generic;
using voltaire.Models.DataObjects;
using System.Linq;

namespace voltaire.Models
{
    public static class ProductConstants
    {

        public static void Init()
        {
            
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

            if (Constants.CompanyName == "Forestier")
            {
                CurrencyValues.Add(1, "€");
            }
        }

        public async static void GenerateProductList()
        {
            var saddles = await App.storeManager.SaddleStore.GetItemsAsync(false, true);

            var accessory = await App.storeManager.AccessoryStore.GetItemsAsync(false, true);

            var service = await App.storeManager.ServiceStore.GetItemsAsync(false, true);


            List<string> SaddleModel = new List<string>();

            List<string> SaddleColor = new List<string>();

            List<string> SaddleLeather = new List<string>();

            List<string> ServiceModel = new List<string>();

            List<string> AccessoryModel = new List<string>();

            List<string> AccessoryCategory = new List<string>();

            List<string> AccessorySubCategory = new List<string>();

            if (saddles.Any())
            {
                SaddleModel.AddRange(saddles.Select((arg) => string.IsNullOrWhiteSpace(arg.Name) ? "N.A" : arg.Name));
                SaddleColor.AddRange(saddles.Select((arg) => string.IsNullOrWhiteSpace(arg.Color) ? "N.A" : arg.Color));
                SaddleLeather.AddRange(saddles.Select((arg) => string.IsNullOrWhiteSpace(arg.Leather) ? "N.A" : arg.Leather ));
                Saddles.Clear();
               
                Saddles.AddRange(saddles);
            }

            if (service.Any())
            {
                ServiceModel.AddRange(service.Select((arg) => string.IsNullOrWhiteSpace(arg.Name) ? "N.A" : arg.Name));
                Services.Clear();
                Services.AddRange(service);
            }

            if (accessory.Any())
            {
                AccessoryModel.AddRange(accessory.Select((arg) => string.IsNullOrWhiteSpace(arg.Name) ? "N.A" : arg.Name));
                AccessoryCategory.AddRange(accessory.Select( (arg) => string.IsNullOrWhiteSpace(arg.CategoryName) ? "N.A" : arg.CategoryName));
                AccessorySubCategory.AddRange(accessory.Select((arg) => string.IsNullOrWhiteSpace(arg.SubCategoryName) ? "N.A" : arg.SubCategoryName));

                Accessory.Clear();
                Accessory.AddRange(accessory);
            }


            SaddleModel = SaddleModel.Distinct().ToList();

            SaddleColor = SaddleColor.Distinct().ToList();

            SaddleLeather = SaddleLeather.Distinct().ToList();

            ServiceModel = ServiceModel.Distinct().ToList();

            AccessoryModel = AccessoryModel.Distinct().ToList();

            AccessoryCategory = AccessoryCategory.Distinct().ToList();

            AccessorySubCategory = AccessorySubCategory.Distinct().ToList();


            foreach (var item in Saddles)
            {
                if (string.IsNullOrWhiteSpace(item.Name))
                    item.Name = "N.A";
                if (string.IsNullOrWhiteSpace(item.Color))
                    item.Color = "N.A";
                if (string.IsNullOrWhiteSpace(item.Leather))
                    item.Leather = "N.A";
            }

            foreach (var item in Services)
            {
                if (string.IsNullOrWhiteSpace(item.Name))
                    item.Name = "N.A";              
            }

            foreach (var item in Accessory)
            {
                if (string.IsNullOrWhiteSpace(item.Name))
                    item.Name = "N.A";
                if (string.IsNullOrWhiteSpace(item.CategoryName))
                    item.CategoryName = "N.A"; 
                if (string.IsNullOrWhiteSpace(item.SubCategoryName))
                    item.CategoryName = "N.A"; 
            }


            Products.Clear();

            Products.Add(new Product()
            {
                Description = "Saddle",
                Name = "Saddle",
                ProductKind = ProductKind.saddle,
                Properties = new List<ProductProperty>()
                {
                    new ProductProperty(PropertyType.IsPicker) { PropertyName = "Model" , PropertyValue = null, ItemSource = SaddleModel },
                    new ProductProperty(PropertyType.IsPicker) { PropertyName = "Color", PropertyValue = null, ItemSource = SaddleColor },
                    new ProductProperty(PropertyType.IsPicker) { PropertyName = "Leather", PropertyValue = null, ItemSource = SaddleLeather },
                    new ProductProperty(PropertyType.IsLabel) { PropertyName = "Unit Price", PropertyValue = null },
                  
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Rider Name" , PropertyValue = null },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Seat" , PropertyValue = null },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Tree" , PropertyValue = null },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Flaps" , PropertyValue = null },

                    new ProductProperty(PropertyType.IsBoolean){ PropertyName = "2nd Skin" , PropertyValue = null },
                    new ProductProperty(PropertyType.IsBoolean){ PropertyName = "RBQ grained" , PropertyValue = null },

                    new ProductProperty(PropertyType.IsPicker){ PropertyName = "Front Block" , PropertyValue = null, ItemSource = new List<string>(){ "S","M","L","No Blocks" } },
                    new ProductProperty(PropertyType.IsPicker){ PropertyName = "Rear Block" , PropertyValue = null, ItemSource = new List<string>(){ "S", "M", "L", "No Blocks" } },
                    new ProductProperty(PropertyType.IsPicker){ PropertyName = "Panel Base" , PropertyValue = null, ItemSource = new List<string>(){ "XFin","Fin","Pro" } },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "A" , PropertyValue = null },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "B" , PropertyValue = null },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "C" , PropertyValue = null },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "D" , PropertyValue = null },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Comments" , PropertyValue = null },
                    new ProductProperty(PropertyType.IsBoolean){ PropertyName = "NamePlate" , PropertyValue = null },
                    new ProductProperty(PropertyType.IsBoolean){ PropertyName = "Greasing" , PropertyValue = null },
                    new ProductProperty(PropertyType.IsBoolean){ PropertyName = "Sp Saddle" , PropertyValue = null },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Note" , PropertyValue = null },

                }
            });

            Products.Add(new Product()
            {
                Description = "Accessory",
                Name = "Accessory",
                ProductKind = ProductKind.accessory,
                Properties = new List<ProductProperty>()
                {
                    new ProductProperty(PropertyType.IsPicker){ PropertyName = "Category" , PropertyValue = null, AllSource = AccessoryCategory, ItemSource = AccessoryCategory },
                    new ProductProperty(PropertyType.IsPicker){ PropertyName = "Sub Category" , PropertyValue = null,AllSource = AccessorySubCategory, ItemSource = AccessorySubCategory },
                    new ProductProperty(PropertyType.IsPicker){ PropertyName = "Name", PropertyValue = null, AllSource = AccessoryModel, ItemSource = AccessoryModel },
                    new ProductProperty(PropertyType.IsLabel) { PropertyName = "Unit Price", PropertyValue = null },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Note", PropertyValue = null },
                }
            }); 

            Products.Add(new Product()
            {
                Description = "Service",
                Name = "Service",
                ProductKind = ProductKind.service,
                Properties = new List<ProductProperty>()
                {
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Serial number", PropertyValue = null },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Brand", PropertyValue = null},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Model", PropertyValue = null },
                    new ProductProperty(PropertyType.IsPicker) { PropertyName = "Name", PropertyValue = null, ItemSource = ServiceModel },
                    new ProductProperty(PropertyType.IsLabel) { PropertyName = "Unit Price", PropertyValue = null },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Seat size", PropertyValue = null},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Flap size", PropertyValue = null},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Leather", PropertyValue = null},
                    new ProductProperty(PropertyType.IsPicker){ PropertyName = "Color", PropertyValue = null, ItemSource = new List<string>(){ "Light Brown", "Chocolate", "Black" }  },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Category", PropertyValue = null},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Comments", PropertyValue = null},
                }
            });

            Products.Add(new Product()
            {
                Description = "Other",
                Name = "Other",
                ProductKind = ProductKind.other,
                Properties = new List<ProductProperty>()
                {
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Name", PropertyValue = null },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Price", PropertyValue = null, IsNumberKeyboard = true },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Description", PropertyValue = null}
                }

            });

            Products.Add(new Product()
            {
                Description = "TradeIn",
                Name = "TradeIn",
                ProductKind = ProductKind.tradein,
                Properties = new List<ProductProperty>()
                {
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Serial number", PropertyValue = null },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Brand", PropertyValue = null},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Model", PropertyValue = null},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Size", PropertyValue = null},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Flap", PropertyValue = null},
                    new ProductProperty(PropertyType.IsPicker){ PropertyName = "Color", PropertyValue = null, ItemSource = new List<string>(){ "Light Brown", "Chocolate", "Black" } },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Leather", PropertyValue = null},
                    new ProductProperty(PropertyType.IsBoolean) { PropertyName = "Blocks", PropertyValue = null},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Other", PropertyValue = null},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Amount", PropertyValue = null, IsNumberKeyboard = true },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Note", PropertyValue = null},
                }

            });

            Products.Add(new Product()
            {
                Description = "Discount",
                Name = "Discount",
                ProductKind = ProductKind.discount,
                Properties = new List<ProductProperty>()
                {
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Name", PropertyValue = null },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Price", PropertyValue = null, IsNumberKeyboard = true },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Note", PropertyValue = null}
                }
            }); 

        }


        public static Dictionary<long, string> CurrencyValues = new Dictionary<long, string>();

        public static List<Saddle> Saddles = new List<Saddle>();

        public static List<Service> Services = new List<Service>();

        public static List<Accessory> Accessory = new List<Accessory>();

        public static List<Product> Products { get; set; } = new List<Product>() { };

        public static List<Agreement> Agreements { get; set; } = new List<Agreement>();

        public static List<string> SeatTypes { get; set; } = new List<string>() { "Big", "Small", "Medium", "Extra Large" };

        public static List<string> TreeTypes { get; set; } = new List<string>() { "Oak", "Mahogany", "Pine", "Spruce" };

        public static List<string> QuantityRange { get; set; } = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };

        public static List<string> TagList { get; set; } = new List<string>() { "Pro", "Nice", "Late Payment", "Behaviour" };

        public static List<string> ProductStatusRange { get; set; } = new List<string>() { QuotationStatus.sent.ToString(), QuotationStatus.draft.ToString(), QuotationStatus.cancel.ToString(), QuotationStatus.done.ToString(), QuotationStatus.sale.ToString() };
   
    }
}
