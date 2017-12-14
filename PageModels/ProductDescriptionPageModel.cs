using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Rg.Plugins.Popup.Services;
using voltaire.Models;
using voltaire.Models.DataObjects;
using voltaire.PageModels.Base;
using voltaire.PopUps;
using Xamarin.Forms;

namespace voltaire.PageModels
{

    public class ProductDescriptionPageModel : BasePageModel
    {

        public Command BackButton => new Command(async () =>
        {
            await CoreMethods.PopPageModel();
        });

        public Command SaveProduct => new Command(async () =>
        {
            if (Product.Name == "Others")
            {

                var productOdoo = new Product();
                productOdoo.Name = Product.Properties[0].PropertyValue;
                productOdoo.Description = Product.Properties[2].PropertyValue;

                await StoreManager.ProductStore.InsertAsync(product);

                // StoreManager.SaleOrderLineStore.InsertAsync();

            }


            await CoreMethods.PopPageModel();
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

        Product product;
        public Product Product
        {
            get { return product; }
            set
            {
                product = value;

                ProductName = product.Name;

                RaisePropertyChanged();
            }
        }

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

        public override void Init(object initData)
        {
            base.Init(initData);

            var context = initData as Tuple<Product, ProductQuotationModel, bool>;

            if (context == null)
                return;

            Product = context.Item1;
            ProductProperties = context.Item2.ProductProperties;
            IsControlsEnabled = context.Item3;
            // SaleOrderLine = context.Item2;
        }

    }
}
