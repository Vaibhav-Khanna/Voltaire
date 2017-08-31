﻿using System;
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
                if (orderstatusindex != value)
                {
                    orderstatusindex = value;
                 
                    RaisePropertyChanged();
                }
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

        int qtyindex;
        public int QtyIndex
        {
            get { return qtyindex; }
            set
            {
                if (qtyindex != value)
                {                    
                    qtyindex = value;

                    RaisePropertyChanged();
					

                }
            }
        }

        int quantity;
        public int Quantity 
        {
            get { return quantity; }
            set
            {
                quantity = value;
               
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
                    TaxFree = UnitPrice - ((ProductConstants.TaxPercent/100) * UnitPrice);
                }
                else
                {
                    TaxFree = UnitPrice;
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
            QtyIndex = 1; 
            OrderStatusIndex = 1;
            IsTaxApply = false;
            Product = _product;
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
