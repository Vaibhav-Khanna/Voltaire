using System;
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
                Product.State = OrderStatusTypes[value];

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
                if (value >= MinimumQuantity)
                {
                    quantity = value;

                    if (istaxapply)
                    {
                        TaxFree = (UnitPrice - (UnitPrice * (double)(TaxPercent / 100))) * Quantity;
                    }
                    else
                    {
                        TaxFree = UnitPrice * Quantity;
                    }

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

                if (istaxapply)
                {
                    TaxFree = (UnitPrice - (UnitPrice * (double)(TaxPercent / 100))) * Quantity;
                }
                else
                {
                    TaxFree = UnitPrice * Quantity;
                }

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

                if (istaxapply)
                {
                    TaxFree = (UnitPrice - (UnitPrice * (double)(TaxPercent/ 100))) * Quantity;
                }
                else
                {
                    TaxFree = UnitPrice * Quantity;
                }

                if (Product != null)
                {
                   Product.TaxApplied = IsTaxApply;
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

        double TaxAmount { get; set; }

        public double TaxPercent { get; private set; } = 20;

        public int MinimumQuantity { get; set; } = 1;


        public ProductQuotationModel(SaleOrderLine _product,string currency)
        {
            OrderStatusTypes = new ObservableCollection<string>(ProductConstants.ProductStatusRange);

            CurrencyLogo = currency;

            QuantitySource = ProductConstants.QuantityRange;

            Init(_product);

            Product = _product;

            UnitPrice = _product.PriceUnit;
           
            Quantity = (int)_product.ProductQty;

            IsTaxApply = _product.TaxApplied;

            GetTax();

            if (OrderStatusTypes.Contains(_product.State?.Trim()?.ToLower()))
                OrderStatusIndex = OrderStatusTypes.IndexOf(_product.State?.Trim()?.ToLower());
            else
                OrderStatusIndex = 1;  

            SetOrderStatusIndex(OrderStatusIndex);
        }

        async void GetTax()
        {
            var tax =  await App.storeManager.AccountTaxStore.GetItemsByExternalId(product.TaxId);

            if (tax != null)
            {
                TaxPercent = tax.Amount;
                TaxAmount = product.PriceUnit * (double)(tax.Amount/100);
            }
        }


        void SetOrderStatusIndex(int _value)
        {
            Xamarin.Forms.Device.StartTimer(new TimeSpan(0, 0, 0, 1, 200), () =>
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
