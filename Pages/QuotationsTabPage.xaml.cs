using System;
using System.Collections.Generic;
using Xamarin.Forms;
using voltaire.Pages.Base;
using voltaire.PageModels;

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
       
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var context = BindingContext as QuotationsPageModel;

            if (context == null)
                return;

            bt_add.Clicked += (sender, e) => 
            {
                context.AddQuotation.Execute(NavigationService); 
            };

			search_bar.TextChanged += (sender, e) => 
            {
                context.SearchQuery.Execute(null);
            };
        }
       
    }
}
