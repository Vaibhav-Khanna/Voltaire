using System;
using System.Collections.Generic;
using Xamarin.Forms;
using voltaire.Pages.Base;
using voltaire.PageModels;
using FreshMvvm;
using voltaire.Models;
using System.Threading.Tasks;

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
            var context = BindingContext as QuotationsPageModel;
            context.TapQuotation.Execute(new Tuple<IPageModelCoreMethods,QuotationsModel>(NavigationService,(QuotationsModel)e.Item ));
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

        void Handle_Refreshing(object sender, System.EventArgs e)
        {     
			var context = BindingContext as QuotationsPageModel;

			if (context == null)
				return;
            
            context.Init(context.Customer);

            listview.EndRefresh();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
       
    }
}
