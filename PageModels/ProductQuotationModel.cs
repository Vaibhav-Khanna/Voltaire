﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using voltaire.Models;
using voltaire.Models.DataObjects;
using Newtonsoft.Json;

namespace voltaire.PageModels
{

    public class ProductQuotationModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public ProductKind ProductKind { get; set; }

        public string CurrencyLogo { get; set; }

        string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;

                if(Product!=null)
                Product.DisplayName = value;

                RaisePropertyChanged();
            }
        }

        SaleOrderLine product;
        public SaleOrderLine Product
        {
            get { return product; }
            set
            {
                product = value;
                RaisePropertyChanged();
            }
        }

       
        ObservableCollection<string> orderstatustypes;
        public ObservableCollection<string> OrderStatusTypes
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


                if (Product != null)
                {
                    Product.State = OrderStatusTypes[value];
                    OrderStatus = OrderStatusTypes[value];
                }


                RaisePropertyChanged();
            }
        }

        string orderstatus;
        public string OrderStatus
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
                if (value >= MinimumQuantity)
                {
                    quantity = value;

                    TaxFree = (UnitPrice - (UnitPrice * (double)(TaxPercent / 100))) * Quantity;

                    TaxIncluded = UnitPrice * Quantity;

                    if (Product != null)
                        Product.ProductQty = quantity;
                }
                else
                    quantity = MinimumQuantity;

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

                TaxFree = (UnitPrice - (UnitPrice * (double)(TaxPercent / 100))) * Quantity;

                TaxIncluded = UnitPrice * Quantity;

                if (Product != null)
                Product.PriceUnit = value;

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

                TaxFree = (UnitPrice - (UnitPrice * (double)(TaxPercent / 100))) * Quantity;

                TaxIncluded = UnitPrice * Quantity;

                if (Product != null)
                {
                    Product.TaxApplied = Convert.ToInt32(TaxPercent) != 0;
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

        double taxincluded;
        public double TaxIncluded
        {
            get { return taxincluded; }
            set
            {
                taxincluded = value;
                RaisePropertyChanged();
            }
        }

        bool canedit = true;
        public bool CanEdit
        {
            get { return canedit; }
            set
            {
                canedit = value;
                RaisePropertyChanged();
            }
        }


        public double TaxPercent { get; set; } = 0;

        public int MinimumQuantity { get; set; } = 1;


        public ProductQuotationModel(SaleOrderLine _product,string currency)
        {
        
            switch (_product.ProductKind)
            {
                case "saddle":
                    {
                        OrderStatusTypes = new ObservableCollection<string>(ProductConstants.ProductStatusRangeSaddle);
                        break;
                    }
                case "accessory":
                    {
                        OrderStatusTypes = new ObservableCollection<string>(ProductConstants.ProductStatusRangeAccessories);
                        break;
                    }
                case "other":
                    {
                        OrderStatusTypes = new ObservableCollection<string>(ProductConstants.ProductStatusRangeOther);
                        break;
                    }
                case "service":
                    {
                        OrderStatusTypes = new ObservableCollection<string>(ProductConstants.ProductStatusRangeService);
                        break;
                    }
                case "discount":
                    {
                        OrderStatusTypes = new ObservableCollection<string>(ProductConstants.ProductStatusRangeDiscount);
                        break;
                    }
                case "tradein":
                    {
                        OrderStatusTypes = new ObservableCollection<string>(ProductConstants.ProductStatusRangeTradeIn);
                        break;
                    }
                default:
                    OrderStatusTypes = new ObservableCollection<string>(ProductConstants.ProductStatusRangeOther);
                    break;                
            }

            CurrencyLogo = currency;

            QuantitySource = ProductConstants.QuantityRange;

            Init(_product);

            Product = _product;

            UnitPrice = _product.PriceUnit;
           
            Quantity = (int)_product.ProductQty;

            ProductKind = QuotationSignPageModel.ParseEnum<ProductKind>(_product.ProductKind);

            if (OrderStatusTypes.Contains(_product.State?.Trim()))
                OrderStatusIndex = OrderStatusTypes.IndexOf(_product.State?.Trim());
            else
                OrderStatusIndex = 0; 

        }


        void SetQuantityIndex(int _value)
        {
            Xamarin.Forms.Device.StartTimer(new TimeSpan(0, 0, 0, 0, 600), () =>
            {
                Quantity = _value;
                return false;
            });
        }

        void Init(SaleOrderLine _product)
        {
            Description = _product.DisplayName;
        }

        void RaisePropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
