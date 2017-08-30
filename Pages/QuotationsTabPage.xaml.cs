using System;
using System.Collections.Generic;
using Xamarin.Forms;
using voltaire.Pages.Base;

namespace voltaire.Pages
{
    public partial class QuotationsTabPage : BaseViewPagerPage
    {
        public QuotationsTabPage()
        {
            InitializeComponent();

            #region UI_tweaks

            listview.ItemTapped += Listview_ItemTapped;

            #endregion


        }

        void Listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            listview.SelectedItem = null;
        }
       

        protected override void BindingContextSet()
        {
            base.BindingContextSet();

        }
    }
}
