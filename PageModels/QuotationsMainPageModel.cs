using System;
using System.Collections.ObjectModel;
using voltaire.Models;
using voltaire.PageModels.Base;
using Xamarin.Forms;
using System.Collections.Generic;
using voltaire.PopUps;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using voltaire.Models.DataObjects;

namespace voltaire.PageModels
{

    public class QuotationsMainPageModel : BasePageModel
    {

        private long TotalCountQuotations;
        private long TotalCountOrders;


        AddCustomerPopUpModel popup_context; //  Popup picker model 
      
        public Command AddQuotation => new Command(async (obj) =>
        {
            popup_context = new AddCustomerPopUpModel(false);

            popup_context.ItemSelectedChanged += Popup_Context_ItemSelectedChanged;    // Subscribe to the event

            await PopupNavigation.PushAsync(new AddCustomerPopUp() { BindingContext = popup_context }, true);
        });


        async void Popup_Context_ItemSelectedChanged()
        {
            if (popup_context.SelectedItem != null)
            {
                await CoreMethods.PushPageModel<QuotationDetailViewPageModel>(new Tuple<Partner, bool, QuotationsModel>(popup_context.SelectedItem, true, null));
            }
            // Unsubscribe from the event
            popup_context.ItemSelectedChanged -= Popup_Context_ItemSelectedChanged;
        }


        public Command TapQuotation => new Command(async (obj) =>
       {
           // mock data
           if (SelectedSegment == 0)
               await CoreMethods.PushPageModel<QuotationDetailViewPageModel>(new Tuple<Partner, bool, QuotationsModel>(new Partner() { }, false, (obj as QuotationsModel)));
           else
                await CoreMethods.PushPageModel<OrderListDetailPageModel>(new Tuple<Partner, bool, QuotationsModel>(new Partner() { }, false, (obj as QuotationsModel)));
       });


        public Command RefreshCommand => new Command(async() =>
        {
            if (SelectedSegment == 0)
                await FetchGlobalQuotations(true);
            else
                await FetchGlobalOrders(true);

            IsRefreshing = false;
        });

        public Command LoadMore => new Command( async() =>
        {
            if (IsLoadMore)
                return;

            IsLoadMore = true;

            if (SelectedSegment == 0)
            {
                if (TotalCountQuotations > 50 && quotationsitemsource.Count < TotalCountQuotations) // Only load if these conditions are met.
                {              
                    var result = await StoreManager.SaleOrderStore.GetQuotations(quotationsitemsource.Count);

                    if ((result as IQueryResultEnumerable<SaleOrder>) != null)
                    {
                        TotalCountQuotations = (result as IQueryResultEnumerable<SaleOrder>).TotalCount;

                        List<QuotationsModel> Temp_Quoatation_list = new List<QuotationsModel>(quotationsitemsource);

                        foreach (var item in result)
                        {
                            Temp_Quoatation_list.Add(new QuotationsModel(item));
                        }

                        foreach (var item in Temp_Quoatation_list)
                        {
                            item.BackColor = Temp_Quoatation_list.IndexOf(item) % 2 == 0 ? Color.FromRgb(247, 247, 247) : Color.White;
                        }

                        quotationsitemsource = new ObservableCollection<QuotationsModel>(Temp_Quoatation_list);
                        RaisePropertyChanged("QuotationsItemSource");
                    }
                }
            }
            else
            {
                if (TotalCountOrders > 50 && ordersitemsource.Count < TotalCountOrders) // Only load if these conditions are met.
                {
                    var result = await StoreManager.SaleOrderStore.GetQuotations(ordersitemsource.Count);

                    if ((result as IQueryResultEnumerable<SaleOrder>) != null)
                    {
                        TotalCountOrders = (result as IQueryResultEnumerable<SaleOrder>).TotalCount;

                        List<QuotationsModel> Temp_Quoatation_list = new List<QuotationsModel>(ordersitemsource);

                        foreach (var item in result)
                        {
                            Temp_Quoatation_list.Add(new QuotationsModel(item));
                        }

                        foreach (var item in Temp_Quoatation_list)
                        {
                            item.BackColor = Temp_Quoatation_list.IndexOf(item) % 2 == 0 ? Color.FromRgb(247, 247, 247) : Color.White;
                        }

                        ordersitemsource = new ObservableCollection<QuotationsModel>(Temp_Quoatation_list);
                        RaisePropertyChanged("QuotationsItemSource");
                    }
                }
            }

            IsLoadMore = false;
        });


        ObservableCollection<QuotationsModel> ordersitemsource;
        ObservableCollection<QuotationsModel> quotationsitemsource;

        public ObservableCollection<QuotationsModel> QuotationsItemSource
        {
            get { return SelectedSegment == 0 ? quotationsitemsource : ordersitemsource; }
            set
            {
                if (SelectedSegment == 0)
                    quotationsitemsource = value;
                else
                    ordersitemsource = value;

                RaisePropertyChanged();
            }
        }

        int _segment;
        public int SelectedSegment
        {
            get { return _segment; }
            set
            {
                _segment = value;

                RaisePropertyChanged();

                RaisePropertyChanged("QuotationsItemSource");
            }
        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            await FetchGlobalQuotations(false);

            await FetchGlobalOrders(false);
        }

        public async override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);

            if(returnedData!=null && returnedData is QuotationsModel)
            {
                await FetchGlobalQuotations(false);

                await FetchGlobalOrders(false);
            }
        }


        async Task FetchGlobalQuotations(bool IsRefresh)
        {
            if (!IsRefresh)
            {
                IsLoading = true;
            }

            var result = await StoreManager.SaleOrderStore.GetQuotations(0);

            if ((result as IQueryResultEnumerable<SaleOrder>) != null)
            {
                TotalCountQuotations = (result as IQueryResultEnumerable<SaleOrder>).TotalCount;

                List<QuotationsModel> Temp_Quoatation_list = new List<QuotationsModel>();

                foreach (var item in result)
                {
                    Temp_Quoatation_list.Add(new QuotationsModel(item));
                }

                foreach (var item in Temp_Quoatation_list)
                {
                    item.BackColor = Temp_Quoatation_list.IndexOf(item) % 2 == 0 ? Color.FromRgb(247, 247, 247) : Color.White;
                }

                quotationsitemsource = new ObservableCollection<QuotationsModel>(Temp_Quoatation_list);
                RaisePropertyChanged("QuotationsItemSource");
            }

            if (!IsRefresh)
                IsLoading = false;
        }

        async Task FetchGlobalOrders(bool IsRefresh)
        {
            if (!IsRefresh)
                IsLoading = true;

            var result = await StoreManager.SaleOrderStore.GetOrders(0);

            if ((result as IQueryResultEnumerable<SaleOrder>) != null)
            {
                TotalCountOrders = (result as IQueryResultEnumerable<SaleOrder>).TotalCount;

                List<QuotationsModel> Temp_Quoatation_list = new List<QuotationsModel>();

                foreach (var item in result)
                {
                    Temp_Quoatation_list.Add(new QuotationsModel(item));
                }

                foreach (var item in Temp_Quoatation_list)
                {
                    item.BackColor = Temp_Quoatation_list.IndexOf(item) % 2 == 0 ? Color.FromRgb(247, 247, 247) : Color.White;
                }

                ordersitemsource = new ObservableCollection<QuotationsModel>(Temp_Quoatation_list);
                RaisePropertyChanged("QuotationsItemSource");
            }

            if (!IsRefresh)
                IsLoading = false;
        }

    }
}
