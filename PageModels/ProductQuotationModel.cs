using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using voltaire.Models;

namespace voltaire.PageModels
{
    
    public class ProductQuotationModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
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
                Init(product);
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

        List<string> orderstatustypes;
        public List<string> OrderStatusTypes
        {
            get { return orderstatustypes; }
            set
            {
                orderstatustypes = value;

                RaisePropertyChanged();
            }
        }

        int orderstatusindex;
        public int OrderStatusIndex
        {
            get { return orderstatusindex; }
            set
            {
                    orderstatusindex = value;
                    
                    RaisePropertyChanged();              
            }
        }

		QuotationStatus orderstatus;
		public QuotationStatus OrderStatus
		{
			get { return orderstatus; }
			set
			{
				orderstatus = value;
				
				RaisePropertyChanged();
			}
		}

        List<string> quantitysource;
        public List<string> QuantitySource
        {
            get { return quantitysource; }
            set
            {
                quantitysource = value;

                RaisePropertyChanged();
            }
        }

        int quantity;
        public int Quantity 
        {
            get { return quantity; }
            set
            {
                quantity = value;

				if (istaxapply)
				{
					TaxFree = (UnitPrice - ((ProductConstants.TaxPercent / 100) * UnitPrice)) * Quantity;
				}
				else
				{
					TaxFree = UnitPrice * Quantity;
				}

                RaisePropertyChanged();
            }
        }



        double unitprice;
        public double UnitPrice 
        { 
            get { return unitprice; }
            set
            { 
                unitprice = value;
               
                RaisePropertyChanged();
            }
        }

        bool istaxapply;
        public bool IsTaxApply 
        {
            get { return istaxapply; }
            set 
            {
                istaxapply = value;

                if(istaxapply)
                {
                    TaxFree = (UnitPrice - ((ProductConstants.TaxPercent/100) * UnitPrice))*Quantity;
                }
                else
                {
                    TaxFree = UnitPrice*Quantity;
                }

                RaisePropertyChanged();
            }
        }

        double taxfree;
        public double TaxFree 
        {
            get { return taxfree; }
            set 
            {
                taxfree = value;
                RaisePropertyChanged();
            }
        }

        public ProductQuotationModel(Product _product)
        {              
            OrderStatusTypes = ProductConstants.ProductStatusRange;
            QuantitySource = ProductConstants.QuantityRange;
            Init(_product);
            IsTaxApply = false;
            Product = _product;

            if(ProductProperties==null)
            {
                var properties = new List<ProductProperty>();
                foreach (var item in _product.Properties)
                {
                    ProductProperty clone = item.ObjectClone(item);
                    properties.Add(clone);
                }
                ProductProperties = properties;
            }
        }

        void SetOrderStatusIndex(int _value)
        {
            Xamarin.Forms.Device.StartTimer(new TimeSpan(0,0,0,1,200), ()=>
            {
                OrderStatusIndex = _value;
                return false;
            });
        }

		void SetQuantityIndex(int _value)
		{
			Xamarin.Forms.Device.StartTimer(new TimeSpan(0, 0, 0, 0, 600), () =>
			{
                Quantity = _value;
				return false;
			});
		}

        void Init(Product _product)
        {
            Description = _product.Description;
            UnitPrice = _product.UnitPrice;
        }

		void RaisePropertyChanged([CallerMemberName] string name = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

    }
}
