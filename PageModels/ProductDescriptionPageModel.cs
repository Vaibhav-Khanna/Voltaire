using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Rg.Plugins.Popup.Services;
using voltaire.Models;
using voltaire.Models.DataObjects;
using voltaire.PageModels.Base;
using voltaire.PopUps;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Linq;

namespace voltaire.PageModels
{

    public class ProductDescriptionPageModel : BasePageModel
    {

        public Command BackButton => new Command(async () =>
        {
            UnsubscribeToEvents();
           
            await CoreMethods.PopPageModel();

        });

        public Command SaveProduct => new Command(async () =>
        {            
            var isCalculated = CalculatePrice(true);

            if (isCalculated)
            {
              
                UnsubscribeToEvents();

                SaleOrderLine.ConfigurationDetail = JsonConvert.SerializeObject(ProductProperties);

                StoreManager.SaleOrderLineStore.UpdateAsync(SaleOrderLine);

                await CoreMethods.PopPageModel();
            }
            else
            {
                if (ProductProperties.Where((arg) => arg.PropertyName == "Unit Price").Any())
                ProductProperties.Where((arg) => arg.PropertyName == "Unit Price").First().PropertyValue = "Not Found";
            }      

        });


        string productname;
        public string ProductName
        {
            get { return productname; }
            set
            {
                productname = value;
                RaisePropertyChanged();
            }
        }

        ProductKind productKind { get; set; }

        List<ProductProperty> productproperties;
        public List<ProductProperty> ProductProperties
        {
            get { return productproperties; }
            set
            {
                productproperties = value;
                RaisePropertyChanged();
            }
        }

        bool iscontrolsenabled;
        public bool IsControlsEnabled
        {
            get { return iscontrolsenabled; }
            set
            {
                iscontrolsenabled = value;
                RaisePropertyChanged();
            }
        }

        SaleOrderLine saleOrderLine;
        public SaleOrderLine SaleOrderLine
        {
            get { return saleOrderLine; }
            set
            {
                saleOrderLine = value;
                RaisePropertyChanged();
            }
        }

        ProductQuotationModel productModel;
        public ProductQuotationModel ProductModel
        {
            get { return productModel; }
            set
            {
                productModel = value;
                RaisePropertyChanged();
            }
        }


        public override void Init(object initData)
        {
            base.Init(initData);

            var context = initData as Tuple<ProductQuotationModel, bool>;

            if (context != null)
            {
                ProductName = context.Item1.Product.ProductKind;

                ProductModel = context.Item1;

                SaleOrderLine = context.Item1.Product;                  

                IsControlsEnabled = context.Item2;

                try
                {
                    if (!string.IsNullOrWhiteSpace(context.Item1.Product.ConfigurationDetail))
                        ProductProperties = JsonConvert.DeserializeObject<List<ProductProperty>>(context.Item1.Product.ConfigurationDetail);
                }
                catch(Exception)
                {
                    
                }

                if(ProductProperties!=null && ProductProperties.Any())
                {
                    foreach (var item in ProductProperties)
                    {
                        if(item.PropertyType == PropertyType.IsPicker)
                        item.PropertyChanged += Handle_PropertyChanged;
                    }                   
                }

            }
        }

        void Handle_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            if (saleOrderLine.ProductKind == ProductKind.accessory.ToString())
            {
                var item = sender as ProductProperty;

                if (item.PropertyValue != null)
                {
                    if (item.PropertyName == "Category")
                    {
                        var data = ProductProperties.Where((arg) => arg.PropertyName == "Sub Category");

                        var data_name = ProductProperties.Where((arg) => arg.PropertyName == "Name");

                        if (data.Any())
                        {
                            var subCat = data.First();

                            subCat.ItemSource = ProductConstants.Accessory.Where((Accessory arg) => arg.CategoryName == item.PropertyValue).Select((arg) => string.IsNullOrWhiteSpace(arg.SubCategoryName) ? "N.A" : arg.SubCategoryName).ToList();

                            subCat.PropertyValue = null;                               
                        }

                        if (data_name.Any())
                        {
                            data_name.First().PropertyValue = null;
                        }
                    }
                    else if (item.PropertyName == "Sub Category")
                    {
                        var data = ProductProperties.Where((arg) => arg.PropertyName == "Name");

                        if (data.Any())
                        {
                            var name = data.First();

                            name.ItemSource = ProductConstants.Accessory.Where((Accessory arg) => arg.SubCategoryName == item.PropertyValue).Select((arg) => string.IsNullOrWhiteSpace(arg.Name) ? "N.A" : arg.Name).ToList();

                            name.PropertyValue = null;
                        }
                    }
                }
            }


            var price = CalculatePrice(false);

            if (!price)
            {
                if(ProductProperties.Where((arg) => arg.PropertyName == "Unit Price").Any())
                ProductProperties.Where((arg) => arg.PropertyName == "Unit Price").First().PropertyValue = "Not Found";
            }

        }


        bool CalculatePrice(bool shouldDisplayPopup)
        {
            if (saleOrderLine == null || saleOrderLine?.ProductKind == null) 
                return false;

            if(saleOrderLine.ProductKind == ProductKind.saddle.ToString())
            {
                ProductProperty ModelProperty = null;
                ProductProperty ColorProperty = null;
                ProductProperty LeatherProperty = null;

                var models = ProductProperties.Where( (arg) => arg.PropertyName == "Model" );

                if (models.Any())
                    ModelProperty = models.First();

                var colors = ProductProperties.Where((arg) => arg.PropertyName == "Color");

                if (colors.Any())
                    ColorProperty = colors.First();

                var leathers = ProductProperties.Where((arg) => arg.PropertyName == "Leather");

                if (leathers.Any())
                    LeatherProperty = leathers.First();

                if (ModelProperty == null || ColorProperty == null || LeatherProperty == null)
                    return false;

                if (ModelProperty.PropertyValue == null)
                {
                    if(shouldDisplayPopup)
                    CoreMethods.DisplayAlert("Alert", "Please select " + ModelProperty.PropertyName, "Ok");
                   
                    return false;
                }

                if (ColorProperty.PropertyValue == null)
                {
                    if (shouldDisplayPopup)
                    CoreMethods.DisplayAlert("Alert", "Please select " + ColorProperty.PropertyName, "Ok");
                    
                    return false;
                }

                if (LeatherProperty.PropertyValue == null)
                {
                    if (shouldDisplayPopup)
                    CoreMethods.DisplayAlert("Alert", "Please select " + LeatherProperty.PropertyName, "Ok");
                    
                    return false;
                }

                var items = ProductConstants.Saddles.Where( (arg) => arg.Color == ColorProperty.PropertyValue && arg.Leather == LeatherProperty.PropertyValue && ModelProperty.PropertyValue == arg.Name );

                if(items==null|| !items.Any())
                {
                    if (shouldDisplayPopup)
                    CoreMethods.DisplayAlert("Alert","The given combination of "+ ModelProperty.PropertyName + "," + ColorProperty.PropertyName + "," + LeatherProperty.PropertyName + " does not exist in pricing. Please choose another one.","OK");
                    
                    return false;
                }
                else
                {
                    if(items.Count() >1)
                    {
                        var item = items.OrderBy((arg) => arg.DateStart).Last();

                        if (shouldDisplayPopup)
                        {
                          
                            productModel.MinimumQuantity = Convert.ToInt32(item.MinQuantity);

                            if (productModel.Quantity < Convert.ToInt32(item.MinQuantity))
                                productModel.Quantity = Convert.ToInt32(item.MinQuantity);

                            ProductModel.UnitPrice = item.Price;

                            ProductModel.Description = item.Name + "/ " + item.Color + "/ " + item.Leather;
                        }

                        ProductProperties.Where( (arg) => arg.PropertyName == "Unit Price").First().PropertyValue = item.Price.ToString() + productModel.CurrencyLogo;
                      
                    }
                    else
                    {
                        if (shouldDisplayPopup)
                        {
                            productModel.MinimumQuantity = Convert.ToInt32(items.First().MinQuantity);

                            if (productModel.Quantity < Convert.ToInt32(items.First().MinQuantity))
                                productModel.Quantity = Convert.ToInt32(items.First().MinQuantity);

                            ProductModel.UnitPrice = items.First().Price;
                            productModel.Description = items.First().Name + "/ " + items.First().Color + "/ " + items.First().Leather;
                        }

                        ProductProperties.Where((arg) => arg.PropertyName == "Unit Price").First().PropertyValue = items.First().Price.ToString() + productModel.CurrencyLogo;

                    }

                    return true;
                }

            }
            else if( saleOrderLine.ProductKind == ProductKind.accessory.ToString())
            {
                ProductProperty AccessoryNameProperty = null;
                ProductProperty AccessoryCategoryProperty = null;
                ProductProperty AccessorySubCategoryProperty = null;

                var categories = ProductProperties.Where((arg) => arg.PropertyName == "Category");

                if (categories.Any())
                    AccessoryCategoryProperty = categories.First();

                var subcategories = ProductProperties.Where((arg) => arg.PropertyName == "Sub Category");

                if (subcategories.Any())
                    AccessorySubCategoryProperty = subcategories.First();

                var names = ProductProperties.Where((arg) => arg.PropertyName == "Name");

                if (names.Any())
                    AccessoryNameProperty = names.First();          

            

                if (AccessoryNameProperty == null || AccessoryCategoryProperty == null || AccessorySubCategoryProperty==null)
                    return false;

                if (AccessoryNameProperty.PropertyValue == null)
                {
                    if (shouldDisplayPopup)
                    CoreMethods.DisplayAlert("Alert", "Please select " + AccessoryNameProperty.PropertyName, "Ok");
                    
                    return false;
                }

                if (AccessoryCategoryProperty.PropertyValue == null)
                {
                    if (shouldDisplayPopup)
                    CoreMethods.DisplayAlert("Alert", "Please select " + AccessoryCategoryProperty.PropertyName, "Ok");
                    
                    return false;
                }

                if (AccessorySubCategoryProperty.PropertyValue == null)
                {
                    if (shouldDisplayPopup)
                        CoreMethods.DisplayAlert("Alert", "Please select " + AccessorySubCategoryProperty.PropertyName, "Ok");

                    return false;
                }

                var items = ProductConstants.Accessory.Where((arg) => arg.SubCategoryName == AccessorySubCategoryProperty.PropertyValue && arg.CategoryName == AccessoryCategoryProperty.PropertyValue && AccessoryNameProperty.PropertyValue == arg.Name);


                if (items == null || !items.Any())
                {
                    if (shouldDisplayPopup)
                        CoreMethods.DisplayAlert("Alert", "The given combination of " + AccessoryNameProperty.PropertyName + "," + AccessorySubCategoryProperty.PropertyName + "," + AccessoryCategoryProperty.PropertyName  + " does not exist in pricing. Please choose another one.", "OK");
                    
                    return false;
                }
                else
                {
                    if (items.Count() > 1)
                    {
                        var item = items.OrderBy((arg) => arg.DateStart).Last();

                        if (shouldDisplayPopup)
                        {
                            productModel.MinimumQuantity = Convert.ToInt32(item.MinQuantity);

                            if (productModel.Quantity < Convert.ToInt32(item.MinQuantity))
                                productModel.Quantity = Convert.ToInt32(item.MinQuantity);

                            ProductModel.UnitPrice = item.Price;
                            productModel.Description = item.Name + "/ " + item.SubCategoryName + "/ " + item.CategoryName;
                        }

                        ProductProperties.Where((arg) => arg.PropertyName == "Unit Price").First().PropertyValue = item.Price.ToString() + productModel.CurrencyLogo;
                                
                    }
                    else
                    {
                        if (shouldDisplayPopup)
                        {
                            productModel.MinimumQuantity = Convert.ToInt32(items.First().MinQuantity);

                            if (productModel.Quantity < Convert.ToInt32(items.First().MinQuantity))
                                productModel.Quantity = Convert.ToInt32(items.First().MinQuantity);

                            ProductModel.UnitPrice = items.First().Price;
                            productModel.Description = items.First().Name + "/ " + items.First().SubCategoryName + "/ " + items.First().CategoryName;
                        }

                        ProductProperties.Where((arg) => arg.PropertyName == "Unit Price").First().PropertyValue = items.First().Price.ToString() + productModel.CurrencyLogo;
                       
                    }

                    return true;
                }
            }
            else if(saleOrderLine.ProductKind == ProductKind.service.ToString())
            {
                ProductProperty ServiceNameProperty = null;
                      
                var names = ProductProperties.Where((arg) => arg.PropertyName == "Name");

                if (names.Any())
                    ServiceNameProperty = names.First();

                if (ServiceNameProperty == null)
                    return false;


                if (ServiceNameProperty.PropertyValue == null)
                {
                    if (shouldDisplayPopup)
                        CoreMethods.DisplayAlert("Alert", "Please select " + ServiceNameProperty.PropertyName, "Ok");

                    return false;
                }          

                var items = ProductConstants.Services.Where((arg) =>  ServiceNameProperty.PropertyValue == arg.Name );


                if (items == null || !items.Any())
                {
                    if (shouldDisplayPopup)
                        CoreMethods.DisplayAlert("Alert", "The given combination of " + ServiceNameProperty.PropertyName +  " does not exist in pricing. Please choose another one.", "OK");

                    return false;
                }
                else
                {
                    if (items.Count() > 1)
                    {
                        var item = items.OrderBy((arg) => arg.DateStart).Last();

                        if (shouldDisplayPopup)
                        {
                            productModel.MinimumQuantity = Convert.ToInt32(item.MinQuantity);

                            if (productModel.Quantity < Convert.ToInt32(item.MinQuantity))
                                productModel.Quantity = Convert.ToInt32(item.MinQuantity);

                            ProductModel.UnitPrice = item.Price;
                            productModel.Description = item.Name;
                        }

                        ProductProperties.Where((arg) => arg.PropertyName == "Unit Price").First().PropertyValue = item.Price.ToString() + productModel.CurrencyLogo;
                    }
                    else
                    {
                        if (shouldDisplayPopup)
                        {
                            productModel.MinimumQuantity = Convert.ToInt32(items.First().MinQuantity);

                            if (productModel.Quantity < Convert.ToInt32(items.First().MinQuantity))
                                productModel.Quantity = Convert.ToInt32(items.First().MinQuantity);

                            ProductModel.UnitPrice = items.First().Price;
                            productModel.Description = items.First().Name;
                        }

                        ProductProperties.Where((arg) => arg.PropertyName == "Unit Price").First().PropertyValue = items.First().Price.ToString() + productModel.CurrencyLogo;

                    }

                    return true;
                }
            }
            else if (saleOrderLine.ProductKind == ProductKind.discount.ToString())
            {
                ProductProperty PriceProperty = null;
                ProductProperty NameProperty = null;

                var names = ProductProperties.Where((arg) => arg.PropertyName == "Price");

                var name = ProductProperties.Where((arg) => arg.PropertyName == "Name");

                if (names.Any())
                    PriceProperty = names.First();

                if (PriceProperty == null)
                    return false;

                if (name.Any())
                    NameProperty = name.First();

                if (NameProperty == null)
                    return false;

                if (string.IsNullOrWhiteSpace(NameProperty.PropertyValue) || string.IsNullOrWhiteSpace(PriceProperty.PropertyValue))
                    return false;

                if (shouldDisplayPopup)
                {
                    productModel.MinimumQuantity = 1;

                    productModel.Quantity = 1;

                    ProductModel.UnitPrice = -Convert.ToInt32(PriceProperty.PropertyValue);
                    productModel.Description = NameProperty.PropertyValue;
                }

            }
            else if (saleOrderLine.ProductKind == ProductKind.tradein.ToString())
            {
                ProductProperty PriceProperty = null;
                ProductProperty NameProperty = null;

                var names = ProductProperties.Where((arg) => arg.PropertyName == "Amount");

                var name = ProductProperties.Where((arg) => arg.PropertyName == "Serial number");

                if (names.Any())
                    PriceProperty = names.First();

                if (PriceProperty == null)
                    return false;

                if (name.Any())
                    NameProperty = name.First();

                if (NameProperty == null)
                    return false;

                if (string.IsNullOrWhiteSpace(NameProperty.PropertyValue) || string.IsNullOrWhiteSpace(PriceProperty.PropertyValue))
                    return false;

                if (shouldDisplayPopup)
                {
                    productModel.MinimumQuantity = 1;

                    productModel.Quantity = 1;

                    ProductModel.UnitPrice = Convert.ToInt32(PriceProperty.PropertyValue);
                    productModel.Description = NameProperty.PropertyValue;
                }
            }
            else if (saleOrderLine.ProductKind == ProductKind.other.ToString())
            {
                ProductProperty PriceProperty = null;
                ProductProperty NameProperty = null;

                var names = ProductProperties.Where((arg) => arg.PropertyName == "Price");

                var name = ProductProperties.Where((arg) => arg.PropertyName == "Name");

                if (names.Any())
                    PriceProperty = names.First();

                if (PriceProperty == null)
                    return false;

                if (name.Any())
                    NameProperty = name.First();

                if (NameProperty == null)
                    return false;

                if (string.IsNullOrWhiteSpace(NameProperty.PropertyValue) || string.IsNullOrWhiteSpace(PriceProperty.PropertyValue))
                    return false;

                if (shouldDisplayPopup)
                {
                    productModel.MinimumQuantity = 1;

                    productModel.Quantity = 1;

                    ProductModel.UnitPrice = Convert.ToInt32(PriceProperty.PropertyValue);
                    productModel.Description = NameProperty.PropertyValue;
                }
            }
            else
            {
                // No case to handle
            }

            return true;
        }

        void UnsubscribeToEvents()
        {
            if (saleOrderLine.ProductKind == ProductKind.accessory.ToString())
            {
                var Cat = ProductProperties.Where((arg) => arg.PropertyName == "Category");

                var subCat = ProductProperties.Where((arg) => arg.PropertyName == "Sub Category");

                var name = ProductProperties.Where((arg) => arg.PropertyName == "Name");

                if (Cat.Any())
                    Cat.First().ItemSource = Cat.First().AllSource;

                if (subCat.Any())
                    subCat.First().ItemSource = subCat.First().AllSource;

                if (name.Any())
                    name.First().ItemSource = name.First().AllSource;
            }

            if (ProductProperties != null && ProductProperties.Any())
            {
                foreach (var item in ProductProperties)
                {
                    if (item.PropertyType == PropertyType.IsPicker)
                        item.PropertyChanged -= Handle_PropertyChanged;
                }
            }

        }
    }
}
