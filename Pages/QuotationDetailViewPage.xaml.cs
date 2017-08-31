using System;
using System.Collections.Generic;
using FreshMvvm;
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
        {listview.SelectedItem = null;
        }
    }
}
