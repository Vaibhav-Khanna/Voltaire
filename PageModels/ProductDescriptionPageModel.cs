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
using voltaire.Resources;

namespace voltaire.PageModels
{

    public class ProductDescriptionPageModel : BasePageModel
    {

        public Command BackButton => new Command(async () =>
        {
            UnsubscribeToEvents();

            if (string.IsNullOrEmpty(productModel.Description))
                await CoreMethods.PopPageModel(productModel);
            else
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
            }
        }

        void Handle_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (saleOrderLine.ProductKind == ProductKind.accessory.ToString())
            {
                var item = sender as ProductProperty;

                if (item.PropertyValue != null)
                {
                    if (item.PropertyName == "Parent Category")
                    {
                        var data = ProductProperties.Where((arg) => arg.PropertyName == "Category");

                        var data_name = ProductProperties.Where((arg) => arg.PropertyName == "Name");

                        if (data.Any())
                        {
                            var subCat = data.First();

                            subCat.ItemSource = ProductConstants.Accessory.Where((Accessory arg) => arg.CategoryName == item.PropertyValue).Select((arg) => string.IsNullOrWhiteSpace(arg.SubCategoryName) ? "N.A" : arg.SubCategoryName).Distinct().OrderBy((arg) => arg).ToList();

                            if(subCat.ItemSource != null && subCat.ItemSource.Any())
                            {
                                
                            }
                            else
                            {
                                subCat.ItemSource = new List<string>() { "N.A" };
                            }

                            subCat.PropertyValue = null;                               
                        }

                        if (data_name.Any())
                        {
                            data_name.First().PropertyValue = null;
                        }
                    }
                    else if (item.PropertyName == "Category")
                    {
                        var data = ProductProperties.Where((arg) => arg.PropertyName == "Name");

                        var cat = ProductProperties.Where((arg) => arg.PropertyName == "Parent Category");

                        if (data.Any() && cat.Any())
                        {
                            var name = data.First();

                            name.ItemSource = ProductConstants.Accessory.Where((Accessory arg) => arg.CategoryName == cat.First().PropertyValue && arg.SubCategoryName == item.PropertyValue).Select((arg) => string.IsNullOrWhiteSpace(arg.Name) ? "N.A" : arg.Name).Distinct().OrderBy((arg) => arg ).ToList();

                            if (name.ItemSource != null && name.ItemSource.Any())
                            {

                            }
                            else
                            {
                                name.ItemSource = new List<string>() { "N.A" };
                            }

                            name.PropertyValue = null;
                        }
                    }
                }
            }
            else if(saleOrderLine.ProductKind == ProductKind.service.ToString())
            {
                var item = sender as ProductProperty;

                if (item.PropertyValue != null)
                {
                    if (item.PropertyName == "After sale service")
                    {
                        var data_name = ProductProperties.Where((arg) => arg.PropertyName == "Service category");

                        if (data_name.Any())
                        {
                            var subCat = data_name.First();

                            if (item.PropertyValue == "true")
                            {
                                subCat.ItemSource = ProductConstants.Services.Where((Service arg) => arg.SubCategoryName == "SAV").Select((arg) => string.IsNullOrWhiteSpace(arg.Name) ? "N.A" : arg.Name).Distinct().ToList();

                                if (subCat.ItemSource != null && subCat.ItemSource.Any())
                                {

                                }
                                else
                                {
                                    subCat.ItemSource = new List<string>() { "N.A" };
                                }
                            }
                            else
                            {
                                subCat.ItemSource = new List<string>() { "N.A" };
                            }

                            subCat.PropertyValue = null;
                        }

                        if (data_name.Any())
                        {
                            data_name.First().PropertyValue = null;
                        }
                    }                 
                }
            }
            else if(saleOrderLine.ProductKind == ProductKind.saddle.ToString())
            {
                var item = sender as ProductProperty;

                if (item.PropertyValue != null)
                {
                    if (item.PropertyName == "Model")
                    {
                        var data_name = ProductProperties.Where((arg) => arg.PropertyName == "Model");

                        if (data_name.Any())
                        {
                            var model_property = data_name.First();

                            if(model_property != null)
                            {
                                
                                var currentModel_attrs = ProductConstants.SaddleModels.Where((arg) => arg.SaddleName?.ToLower()?.Trim() == model_property.PropertyValue?.ToLower()?.Trim());

                                var color_id = ProductConstants.SaddleAttributes.Where((arg) => arg.Code == "c_color")?.First()?.EnUs;

                                var leather_id = ProductConstants.SaddleAttributes.Where((arg) => arg.Code == "c_leather")?.First()?.EnUs;

                                var frontblock_id = ProductConstants.SaddleAttributes.Where((arg) => arg.Code == "c_fblock")?.First()?.EnUs;

                                var rearblock_id = ProductConstants.SaddleAttributes.Where((arg) => arg.Code == "c_rblock")?.First()?.EnUs;

                                var panel_id = ProductConstants.SaddleAttributes.Where((arg) => arg.Code == "c_panel")?.First()?.EnUs;

                                var grained_id = ProductConstants.SaddleAttributes.Where((arg) => arg.Code == "c_grained")?.First()?.EnUs;


                                // replace the attribute id matching with code in each of the below properties

                                var colors = ProductProperties.Where((arg) => arg.PropertyName == "Color").First();
                                colors.ItemSource = new List<string>();
                                colors.PropertyValue = null;

                                if(color_id!=null)
                                {
                                    var data = currentModel_attrs.Where((arg) => arg.AttributeName == color_id);
                                   
                                    if(data.Any())
                                    {
                                        foreach (var _item in data.First().AttributeValueList)
                                        {
                                            colors.ItemSource.Add(ProductConstants.SaddleValues.Where((arg) => arg.Id.ToString() == _item).First().EnUs);
                                        }
                                    }
                                }

                                var leather = ProductProperties.Where((arg) => arg.PropertyName == "Leather").First();
                                leather.ItemSource = new List<string>();
                                leather.PropertyValue = null;

                                if (leather_id != null)
                                {
                                    var data = currentModel_attrs.Where((arg) => arg.AttributeName == leather_id);

                                    if (data.Any())
                                    {
                                        
                                        foreach (var _item in data.First().AttributeValueList)
                                        {
                                            leather.ItemSource.Add(ProductConstants.SaddleValues.Where((arg) => arg.Id.ToString() == _item).First().EnUs);
                                        }
                                    }
                                }

                                var grains = ProductProperties.Where((arg) => arg.PropertyName == "Grained");
                                if (grains != null && grains.Any())
                                {
                                    var grained = grains.First();
                                    grained.ItemSource = new List<string>();
                                    grained.PropertyValue = null;

                                    if (grained_id != null)
                                    {
                                        var data = currentModel_attrs.Where((arg) => arg.AttributeName == grained_id);

                                        if (data.Any())
                                        {
                                            foreach (var _item in data.First().AttributeValueList)
                                            {
                                                grained.ItemSource.Add(ProductConstants.SaddleValues.Where((arg) => arg.Id.ToString() == _item).First().EnUs);
                                            }
                                        }
                                    }
                                }

                                var f_block = ProductProperties.Where((arg) => arg.PropertyName == "Front Block").First();
                                f_block.ItemSource = new List<string>();
                                f_block.PropertyValue = null;

                                if (frontblock_id != null)
                                {
                                    var data = currentModel_attrs.Where((arg) => arg.AttributeName == frontblock_id);

                                    if (data.Any())
                                    {
                                        

                                        foreach (var _item in data.First().AttributeValueList)
                                        {
                                            f_block.ItemSource.Add(ProductConstants.SaddleValues.Where((arg) => arg.Id.ToString() == _item).First().EnUs);
                                        }
                                    }
                                }

                                var r_block = ProductProperties.Where((arg) => arg.PropertyName == "Rear Block").First();
                                r_block.ItemSource = new List<string>();
                                r_block.PropertyValue = null;

                                if (rearblock_id != null)
                                {
                                    var data = currentModel_attrs.Where((arg) => arg.AttributeName == rearblock_id);

                                    if (data.Any())
                                    {
                                        

                                        foreach (var _item in data.First().AttributeValueList)
                                        {
                                            r_block.ItemSource.Add(ProductConstants.SaddleValues.Where((arg) => arg.Id.ToString() == _item).First().EnUs);
                                        }
                                    }
                                }

                                var panel = ProductProperties.Where((arg) => arg.PropertyName == "Panel Base").First();
                                panel.ItemSource = new List<string>();
                                panel.PropertyValue = null;

                                if (panel_id != null)
                                {
                                    var data = currentModel_attrs.Where((arg) => arg.AttributeName == panel_id);

                                    if (data.Any())
                                    {
                                        
                                        foreach (var _item in data.First().AttributeValueList)
                                        {
                                            panel.ItemSource.Add(ProductConstants.SaddleValues.Where((arg) => arg.Id.ToString() == _item).First().EnUs);
                                        }
                                    }
                                }

                            }

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
                ProductProperty GrainedProperty = null;

                var models = ProductProperties.Where( (arg) => arg.PropertyName == "Model" );

                if (models.Any())
                    ModelProperty = models.First();

                var colors = ProductProperties.Where((arg) => arg.PropertyName == "Color");

                if (colors.Any())
                    ColorProperty = colors.First();

                var leathers = ProductProperties.Where((arg) => arg.PropertyName == "Leather");

                if (leathers.Any())
                    LeatherProperty = leathers.First();

                var grained = ProductProperties.Where((arg) => arg.PropertyName == "Grained");

                if (grained.Any())
                    GrainedProperty = grained.First();

                if (ModelProperty == null || LeatherProperty == null )
                    return false;

                if (ModelProperty.PropertyValue == null)
                {
                    if(shouldDisplayPopup)
                        CoreMethods.DisplayAlert(AppResources.Alert, AppResources.PleaseSelect + ModelProperty.PropertyName, AppResources.Ok);
                   
                    return false;
                }

                if (ColorProperty?.PropertyValue == null)
                {
                    if (shouldDisplayPopup)
                    CoreMethods.DisplayAlert(AppResources.Alert, AppResources.PleaseSelect + ColorProperty?.PropertyName, AppResources.Ok);
                    
                    return false;
                }

                if (LeatherProperty.PropertyValue == null)
                {
                    if (shouldDisplayPopup)
                    CoreMethods.DisplayAlert(AppResources.Alert, AppResources.PleaseSelect + LeatherProperty.PropertyName, AppResources.Ok);
                    
                    return false;
                }

                if (GrainedProperty != null && GrainedProperty?.PropertyValue == null)
                {
                    if (shouldDisplayPopup)
                        CoreMethods.DisplayAlert(AppResources.Alert, AppResources.SelectGrained, AppResources.Ok);

                    return false;
                }

                var items = ProductConstants.Saddles.Where( (arg) => arg.Leather == LeatherProperty.PropertyValue && ModelProperty.PropertyValue == arg.Name );

                if(items==null|| !items.Any())
                {
                    if (shouldDisplayPopup)
                    CoreMethods.DisplayAlert(AppResources.Alert, AppResources.GivenCombination+ ModelProperty.PropertyName  + "," + LeatherProperty.PropertyName + AppResources.ChooseAnotherOne,AppResources.Ok);
                    
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
                ProductProperty ReferenceProperty = null;

                var categories = ProductProperties.Where((arg) => arg.PropertyName == "Parent Category");

                if (categories.Any())
                    AccessoryCategoryProperty = categories.First();

                var subcategories = ProductProperties.Where((arg) => arg.PropertyName == "Category");

                if (subcategories.Any())
                    AccessorySubCategoryProperty = subcategories.First();

                var reference = ProductProperties.Where((arg) => arg.PropertyName == "Reference");

                if (reference.Any())
                    ReferenceProperty = reference.First();


                var names = ProductProperties.Where((arg) => arg.PropertyName == "Name");

                if (names.Any())
                    AccessoryNameProperty = names.First();          

            

                if (AccessoryNameProperty == null || AccessoryCategoryProperty == null || AccessorySubCategoryProperty==null)
                    return false;

                if (AccessoryNameProperty.PropertyValue == null)
                {
                    if (shouldDisplayPopup)
                    CoreMethods.DisplayAlert(AppResources.Alert, AppResources.PleaseSelect + AccessoryNameProperty.PropertyName, AppResources.Ok);
                    
                    return false;
                }

                if (AccessoryCategoryProperty.PropertyValue == null)
                {
                    if (shouldDisplayPopup)
                    CoreMethods.DisplayAlert(AppResources.Alert, AppResources.PleaseSelect + AccessoryCategoryProperty.PropertyName, AppResources.Ok);
                    
                    return false;
                }

                if (AccessorySubCategoryProperty.PropertyValue == null)
                {
                    if (shouldDisplayPopup)
                        CoreMethods.DisplayAlert(AppResources.Alert, AppResources.PleaseSelect + AccessorySubCategoryProperty.PropertyName, AppResources.Ok);

                    return false;
                }

                var items = ProductConstants.Accessory.Where((arg) => arg.SubCategoryName == AccessorySubCategoryProperty.PropertyValue && arg.CategoryName == AccessoryCategoryProperty.PropertyValue && AccessoryNameProperty.PropertyValue == arg.Name);


                if (items == null || !items.Any())
                {
                    if (shouldDisplayPopup)
                        CoreMethods.DisplayAlert(AppResources.Alert, AppResources.GivenCombination + AccessoryNameProperty.PropertyName + "," + AccessorySubCategoryProperty.PropertyName + "," + AccessoryCategoryProperty.PropertyName  + AppResources.ChooseAnotherOne, AppResources.Ok);
                    
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
                        if(ReferenceProperty!=null)
                        ReferenceProperty.PropertyValue = item.Reference;
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
                        if (ReferenceProperty != null)
                        ReferenceProperty.PropertyValue = items.First().Reference;
                    }

                    return true;
                }
            }
            else if(saleOrderLine.ProductKind == ProductKind.service.ToString())
            {
                ProductProperty ServiceNameProperty = null;
                ProductProperty ReferenceProperty = null;      

                var names = ProductProperties.Where((arg) => arg.PropertyName == "Service category");

                if (names.Any())
                    ServiceNameProperty = names.First();

                if (ServiceNameProperty == null)
                    return false;

                var reference = ProductProperties.Where((arg) => arg.PropertyName == "Reference");

                if (reference.Any())
                    ReferenceProperty = reference.First();

                if (ServiceNameProperty.PropertyValue == null)
                {
                    if (shouldDisplayPopup)
                        CoreMethods.DisplayAlert(AppResources.Alert, AppResources.PleaseSelect + ServiceNameProperty.PropertyName, AppResources.Ok);

                    return false;
                }          

                var items = ProductConstants.Services.Where((arg) =>  ServiceNameProperty.PropertyValue == arg.Name );


                if (items == null || !items.Any())
                {
                    if (shouldDisplayPopup)
                        CoreMethods.DisplayAlert(AppResources.Alert, AppResources.GivenCombination + ServiceNameProperty.PropertyName +  AppResources.ChooseAnotherOne, AppResources.Ok);

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
                        if (ReferenceProperty != null)
                            ReferenceProperty.PropertyValue = item.Reference;
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
                        if (ReferenceProperty != null)
                        ReferenceProperty.PropertyValue = items.First().Reference;
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

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            if (ProductProperties != null && ProductProperties.Any())
            {
                foreach (var item in ProductProperties)
                {
                    if (item.PropertyType == PropertyType.IsPicker || item.PropertyType == PropertyType.IsBoolean)
                        item.PropertyChanged += Handle_PropertyChanged;
                }
            }
        }

        void UnsubscribeToEvents()
        {
            if (ProductProperties != null && ProductProperties.Any())
            {
                foreach (var item in ProductProperties)
                {
                    if (item.PropertyType == PropertyType.IsPicker || item.PropertyType == PropertyType.IsBoolean)
                        item.PropertyChanged -= Handle_PropertyChanged;
                }
            }

            if (ProductProperties != null && ProductProperties.Any())
                if (saleOrderLine?.ProductKind == ProductKind.accessory.ToString())
                {
                    var Cat = ProductProperties.Where((arg) => arg.PropertyName == "Parent Category");

                    var subCat = ProductProperties.Where((arg) => arg.PropertyName == "Category");

                    var name = ProductProperties.Where((arg) => arg.PropertyName == "Name");

                    if (Cat.Any())
                    {
                        var selected = Cat.First().PropertyValue;
                        Cat.First().ItemSource = Cat.First().AllSource;
                        Cat.First().PropertyValue = selected;
                    }

                    if (subCat.Any())
                    {
                        var selected = subCat.First().PropertyValue;
                        subCat.First().ItemSource = subCat.First().AllSource;
                        subCat.First().PropertyValue = selected;
                    }

                    if (name.Any())
                    {
                        var selected = name.First().PropertyValue;
                        name.First().ItemSource = name.First().AllSource;
                        name.First().PropertyValue = selected;
                    }
                }
        }



    }
}
