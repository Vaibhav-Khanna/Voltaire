using System;
using System.Collections.Generic;
using voltaire.Models.DataObjects;
using System.Linq;
using System.Threading.Tasks;
using voltaire.Helpers;
using voltaire.Resources;

namespace voltaire.Models
{
    public static class ProductConstants
    {

        public static void Init()
        {
            
            //Agreements.Add(new Agreement() { Title = "1) You will have to obey to all the rules and regulations mentioned by the company." });

            CurrencyValues.Clear();

            if (Constants.CompanyName == "Forestier")
            {                
                CurrencyValues.Add(1, "€");
            }
        }

        public async static Task GenerateProductList()
        {
            var saddles = await App.storeManager.SaddleStore.GetItemsAsync(false, true);

            var accessory = await App.storeManager.AccessoryStore.GetItemsAsync(false, true);

            var service = await App.storeManager.ServiceStore.GetItemsAsync(false, true);

            var saddle_attrs = await App.storeManager.SaddleStore.GetSaddleAttributes();

            var saddle_values = await App.storeManager.SaddleStore.GetSaddleValue();

            var saddke_models = await App.storeManager.SaddleStore.GetSaddleModel();


            if (saddle_attrs != null)
                SaddleAttributes.AddRange(saddle_attrs);

            if (saddke_models != null)
                SaddleModels.AddRange(saddke_models);

            if (saddle_values != null)
                SaddleValues.AddRange(saddle_values);


            List<string> SaddleModel = new List<string>();

            List<string> SaddleColor = new List<string>();

            List<string> SaddleLeather = new List<string>();

            List<string> ServiceModel = new List<string>();

            List<string> ServiceSubCategoryModel = new List<string>();

            List<string> AccessoryModel = new List<string>();

            List<string> AccessoryCategory = new List<string>();

            List<string> AccessorySubCategory = new List<string>();


            if (saddles.Any())
            {
                SaddleModel.AddRange(saddles.Select((arg) => string.IsNullOrWhiteSpace(arg.Name) ? "N.A" : arg.Name));
               
                SaddleLeather.AddRange(saddles.Select((arg) => string.IsNullOrWhiteSpace(arg.Leather) ? "N.A" : arg.Leather ));
                Saddles.Clear();
               
                Saddles.AddRange(saddles);
            }

            if (service.Any())
            {
                ServiceModel.AddRange(service.Select((arg) => string.IsNullOrWhiteSpace(arg.Name) ? "N.A" : arg.Name));
                ServiceSubCategoryModel.AddRange(service.Select((arg) => string.IsNullOrWhiteSpace(arg.SubCategoryName) ? "N.A" : arg.SubCategoryName));
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

            SaddleColor.Add("Black");
            SaddleColor.Add("Brown");

            SaddleLeather = SaddleLeather.Distinct().ToList();

            ServiceModel = ServiceModel.Distinct().ToList();

            ServiceSubCategoryModel = ServiceSubCategoryModel.Distinct().ToList();

            AccessoryModel = AccessoryModel.Distinct().ToList();

            AccessoryCategory = AccessoryCategory.Distinct().ToList();

            AccessorySubCategory = AccessorySubCategory.Distinct().ToList();
                      
            foreach (var item in Saddles)
            {
                if (string.IsNullOrWhiteSpace(item.Name))
                    item.Name = "N.A";
                if (string.IsNullOrWhiteSpace(item.Leather))
                    item.Leather = "N.A";
            }

            foreach (var item in Services)
            {
                if (string.IsNullOrWhiteSpace(item.Name))
                    item.Name = "N.A";   
                if (string.IsNullOrWhiteSpace(item.SubCategoryName))
                    item.SubCategoryName = "N.A";   
            }

            foreach (var item in Accessory)
            {
                if (string.IsNullOrWhiteSpace(item.Name))
                    item.Name = "N.A";
                if (string.IsNullOrWhiteSpace(item.CategoryName))
                    item.CategoryName = "N.A"; 
                if (string.IsNullOrWhiteSpace(item.SubCategoryName))
                    item.SubCategoryName = "N.A"; 
            }


            Products.Clear();

            Products.Add(new Product()
            {
                Description = "Saddle",
                Name = "Saddle",
                Name_FR = AppResources.P_Saddle,
                ProductKind = ProductKind.saddle,
                Properties = new List<ProductProperty>()
                {
                    new ProductProperty(PropertyType.IsPicker) { PropertyName = "Model" , PropertyName_FR = AppResources.P_Model , PropertyValue = null, ItemSource = SaddleModel },
                    new ProductProperty(PropertyType.IsPicker) { PropertyName = "Color", PropertyName_FR = AppResources.P_Color },
                    new ProductProperty(PropertyType.IsPicker) { PropertyName = "Leather", PropertyName_FR = AppResources.P_Leather },
                    new ProductProperty(PropertyType.IsPicker) { PropertyName = "Grained",PropertyName_FR = AppResources.P_Grained },
                    new ProductProperty(PropertyType.IsLabel) { PropertyName = "Unit Price",PropertyName_FR = AppResources.P_UnitPrice, PropertyValue = null },

                    new ProductProperty(PropertyType.IsText){ PropertyName = "Rider Name" , PropertyValue = null, PropertyName_FR = AppResources.P_RiderName },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Seat" , PropertyValue = null, PropertyName_FR = AppResources.P_Seat },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Tree" , PropertyValue = null, PropertyName_FR = AppResources.P_Tree },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Flaps" , PropertyValue = null, PropertyName_FR = AppResources.P_Flaps },

                    new ProductProperty(PropertyType.IsBoolean){ PropertyName = "2nd Skin" , PropertyValue = null, PropertyName_FR = AppResources.P_2ndSkin },
                    new ProductProperty(PropertyType.IsBoolean){ PropertyName = "RBQ grained" , PropertyValue = null, PropertyName_FR = AppResources.P_RBQgrained },

                    new ProductProperty(PropertyType.IsPicker){ PropertyName = "Front Block" , PropertyValue = null, PropertyName_FR = AppResources.P_FrontBlock },
                    new ProductProperty(PropertyType.IsPicker){ PropertyName = "Rear Block" , PropertyValue = null, PropertyName_FR = AppResources.P_RearBlock  },
                    new ProductProperty(PropertyType.IsPicker){ PropertyName = "Panel Base" , PropertyValue = null, PropertyName_FR = AppResources.P_PanelBase },

                    new ProductProperty(PropertyType.IsText){ PropertyName = "A" , PropertyValue = null, PropertyName_FR = "A" },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "B" , PropertyValue = null, PropertyName_FR = "B" },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "C" , PropertyValue = null, PropertyName_FR = "C" },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "D" , PropertyValue = null, PropertyName_FR = "D" },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Comments" , PropertyValue = null, PropertyName_FR = AppResources.P_Comments },
                    new ProductProperty(PropertyType.IsBoolean){ PropertyName = "NamePlate" , PropertyValue = null, PropertyName_FR = AppResources.P_NamePlate },
                    new ProductProperty(PropertyType.IsBoolean){ PropertyName = "Greasing" , PropertyValue = null, PropertyName_FR = AppResources.P_Greasing },
                    new ProductProperty(PropertyType.IsBoolean){ PropertyName = "Sp Saddle" , PropertyValue = null, PropertyName_FR = AppResources.P_SpSaddle },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Note" , PropertyValue = null, PropertyName_FR = AppResources.Note }
                }
            });

            Products.Add(new Product()
            {
                Description = "Accessory",
                Name = "Accessory",
                Name_FR = AppResources.P_Accessory,
                ProductKind = ProductKind.accessory,
                Properties = new List<ProductProperty>()
                {
                    new ProductProperty(PropertyType.IsPicker){ PropertyName = "Parent Category" , PropertyValue = null, AllSource = AccessoryCategory, ItemSource = AccessoryCategory, PropertyName_FR = AppResources.P_ParentCategory },
                    new ProductProperty(PropertyType.IsPicker){ PropertyName = "Category" , PropertyValue = null,AllSource = AccessorySubCategory, ItemSource = AccessorySubCategory, PropertyName_FR = AppResources.P_Category },
                    new ProductProperty(PropertyType.IsPicker){ PropertyName = "Name", PropertyValue = null, AllSource = AccessoryModel, ItemSource = AccessoryModel, PropertyName_FR = AppResources.Name },
                    new ProductProperty(PropertyType.IsLabel) { PropertyName = "Unit Price", PropertyValue = null, PropertyName_FR = AppResources.P_UnitPrice },
                    new ProductProperty(PropertyType.IsLabel) { PropertyName = "Reference", PropertyValue = null, IsVisible = false, PropertyName_FR = AppResources.P_Reference },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Note", PropertyValue = null, PropertyName_FR = AppResources.P_Note },
                }
            }); 

            Products.Add(new Product()
            {
                Description = "Service",
                Name = "Service",
                Name_FR = AppResources.P_Service,
                ProductKind = ProductKind.service,
                Properties = new List<ProductProperty>()
                {
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Serial number", PropertyValue = null, PropertyName_FR = AppResources.P_Serial_number },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Brand", PropertyValue = null, PropertyName_FR = AppResources.P_Brand},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Model", PropertyValue = null, PropertyName_FR = AppResources.P_Model },
                    new ProductProperty(PropertyType.IsBoolean) { PropertyName = "After sale service", PropertyValue = null, ItemSource = ServiceSubCategoryModel, PropertyName_FR = AppResources.P_AfterSale },
                    new ProductProperty(PropertyType.IsPicker) { PropertyName = "Service category", PropertyValue = null, AllSource = ServiceModel, ItemSource = new List<string>(){"N.A"}, PropertyName_FR = AppResources.P_ServiceCategory },
                    new ProductProperty(PropertyType.IsLabel) { PropertyName = "Unit Price", PropertyValue = null, PropertyName_FR = AppResources.P_UnitPrice },
                    new ProductProperty(PropertyType.IsLabel) { PropertyName = "Reference", PropertyValue = null, PropertyName_FR = AppResources.P_Reference },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Seat size", PropertyValue = null, PropertyName_FR = AppResources.P_Seatsize},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Flap size", PropertyValue = null, PropertyName_FR = AppResources.P_Flapsize},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Leather", PropertyValue = null, PropertyName_FR = AppResources.P_Leather},
                    new ProductProperty(PropertyType.IsPicker){ PropertyName = "Color", PropertyValue = null, ItemSource = new List<string>(){ "Light Brown", "Chocolate", "Black" }, PropertyName_FR = AppResources.P_Color  },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Category", PropertyValue = null, PropertyName_FR = AppResources.P_Category},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Comments", PropertyValue = null, PropertyName_FR = AppResources.P_Comments},
                }
            });

            Products.Add(new Product()
            {
                Description = "Other",
                Name = "Other",
                Name_FR = AppResources.P_Other,
                ProductKind = ProductKind.other,
                Properties = new List<ProductProperty>()
                {
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Name", PropertyValue = null, PropertyName_FR = AppResources.Name },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Price", PropertyValue = null, IsNumberKeyboard = true, PropertyName_FR = AppResources.P_Price },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Description", PropertyValue = null, PropertyName_FR = AppResources.P_Description}
                }

            });

            Products.Add(new Product()
            {
                Description = "TradeIn",
                Name = "TradeIn",
                Name_FR  = AppResources.P_TradeIn,
                ProductKind = ProductKind.tradein,
                Properties = new List<ProductProperty>()
                {
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Serial number", PropertyValue = null, PropertyName_FR = AppResources.P_Serial_number },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Brand", PropertyValue = null, PropertyName_FR = AppResources.P_Brand},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Model", PropertyValue = null, PropertyName_FR = AppResources.P_Model},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Size", PropertyValue = null, PropertyName_FR = AppResources.P_Size},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Flap", PropertyValue = null, PropertyName_FR = AppResources.P_Flaps},
                    new ProductProperty(PropertyType.IsPicker){ PropertyName = "Color", PropertyValue = null, ItemSource = new List<string>(){ "Light Brown", "Chocolate", "Black" }, PropertyName_FR = AppResources.P_Color },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Leather", PropertyValue = null, PropertyName_FR = AppResources.P_Leather},
                    new ProductProperty(PropertyType.IsBoolean) { PropertyName = "Blocks", PropertyValue = null, PropertyName_FR = AppResources.P_Blocks},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Other", PropertyValue = null, PropertyName_FR = AppResources.P_Other},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Amount", PropertyValue = null, IsNumberKeyboard = true, PropertyName_FR = AppResources.Amount },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Note", PropertyValue = null, PropertyName_FR = AppResources.P_Note},
                }

            });

            Products.Add(new Product()
            {
                Description = "Discount",
                Name = "Discount",
                Name_FR = AppResources.P_Discount,
                ProductKind = ProductKind.discount,
                Properties = new List<ProductProperty>()
                {
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Name", PropertyValue = null, PropertyName_FR = AppResources.Name },
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Price", PropertyValue = null, IsNumberKeyboard = true , PropertyName_FR = AppResources.P_Price},
                    new ProductProperty(PropertyType.IsText){ PropertyName = "Note", PropertyValue = null, PropertyName_FR = AppResources.P_Note }
                }
            }); 

        }


        public static Dictionary<long, string> CurrencyValues = new Dictionary<long, string>();

        public static List<Saddle> Saddles = new List<Saddle>();

        public static List<Service> Services = new List<Service>();

        public static List<Accessory> Accessory = new List<Accessory>();

        public static List<Product> Products { get; set; } = new List<Product>() { };

        //public static List<Agreement> Agreements { get; set; } = new List<Agreement>();

        //public static List<string> SeatTypes { get; set; } = new List<string>() { "Big", "Small", "Medium", "Extra Large" };

        //public static List<string> TreeTypes { get; set; } = new List<string>() { "Oak", "Mahogany", "Pine", "Spruce" };

        public static List<string> QuantityRange { get; set; } = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };

        //public static List<string> TagList { get; set; } = new List<string>() { "Pro", "Nice", "Late Payment", "Behaviour" };

        public static List<string> ProductStatusRangeSaddle { get; set; } = new List<string>() { ProductStatus.Order.ToString() };

        public static List<string> ProductStatusRangeService { get; set; } = new List<string>() { ProductStatus.Delivered.ToString(), ProductStatus.Received.ToString(), ProductStatus.ToBeReceived.ToString() };

        public static List<string> ProductStatusRangeAccessories { get; set; } = new List<string>() { ProductStatus.Order.ToString(), ProductStatus.Delivered.ToString() };


        public static List<string> ProductStatusRangeTradeIn { get; set; } = new List<string>() {  ProductStatus.Received.ToString(), ProductStatus.ToBeReceived.ToString() };


        public static List<string> ProductStatusRangeOther { get; set; } = new List<string>() { ProductStatus.Order.ToString(), ProductStatus.Delivered.ToString(), ProductStatus.Received.ToString(), ProductStatus.ToBeReceived.ToString() };


        public static List<string> ProductStatusRangeDiscount { get; set; } = new List<string>() { " " };


        public static List<SaddleAttribute> SaddleAttributes { get; set; } = new List<SaddleAttribute>();

        public static List<SaddleModel> SaddleModels { get; set; } = new List<SaddleModel>();

        public static List<SaddleValue> SaddleValues { get; set; } = new List<SaddleValue>();

    }
}
