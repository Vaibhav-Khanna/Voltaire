using System;
using System.Collections.Generic;
using FreshMvvm;
using voltaire.PageModels;
using voltaire.Pages.Base;
using Xamarin.Forms;

namespace voltaire.Pages
{
    public partial class QuotationDetailViewPage : FreshBaseContentPage
    {
        public QuotationDetailViewPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            listview.ItemTapped += Listview_ItemTapped;

        }

        void Listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            listview.SelectedItem = null;
        }

        void Handle_BindingContextChanged(object sender, System.EventArgs e)
        {
            var viewcell = sender as ViewCell;

            var grid = viewcell.View as Grid;

            var taxswt = grid.FindByName<Switch>("taxswitch");
            var quantity = grid.FindByName<Entry>("Qty");


            if (BindingContext != null)
            {
                quantity.TextChanged += Quantity_TextChanged;
                taxswt.Toggled += Taxswitch_Toggled;
            }
            else
            {
                taxswt.Toggled -= Taxswitch_Toggled;
                quantity.TextChanged -= Quantity_TextChanged;
            }
        }

        void Taxswitch_Toggled(object sender, ToggledEventArgs e)
        {
            if (BindingContext == null)
                return;
            
            (BindingContext as QuotationDetailViewPageModel).OrderItemsSource_CollectionChanged(null, null);
        }

        void Quantity_TextChanged(object sender, TextChangedEventArgs e)
        {
			if (BindingContext == null)
				return;
            
			(BindingContext as QuotationDetailViewPageModel).OrderItemsSource_CollectionChanged(null, null);
        }
    }
}
