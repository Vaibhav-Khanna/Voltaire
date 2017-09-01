using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Rg.Plugins.Popup.Services;
using voltaire.Models;
using voltaire.PageModels.Base;
using voltaire.PopUps;
using Xamarin.Forms;

namespace voltaire.PageModels
{
    public class ProductDescriptionPageModel : BasePageModel
    {

        public Command BackButton => new Command(async() =>
        {
            await CoreMethods.PopPageModel();
        });

		public Command SaveProduct => new Command(async () =>
		{
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

        public override void Init(object initData)
        {
            base.Init(initData);

            var context = initData as Tuple<Product,ProductQuotationModel>;

            if (context == null)
                return;

            Product = context.Item1;
            ProductProperties = context.Item2.ProductProperties;
        }

    }
}
