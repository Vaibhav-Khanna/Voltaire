using System;
using System.Collections.Generic;
using voltaire.Pages.Base;
using Xamarin.Forms;

namespace voltaire.Pages
{
    public partial class QuotationInternalNotesPage : BaseDisposePage
    {
        public QuotationInternalNotesPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            listview.ItemTapped += Listview_ItemTapped;
        }

        void Listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            listview.SelectedItem = null;
        }

        public override void DisposeResources()
        {
            
        }
    }
}
