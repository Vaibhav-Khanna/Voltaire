using System;
using System.Collections.Generic;
using Xamarin.Forms;
using voltaire.Pages.Base;
using voltaire.PageModels;
using voltaire.Models;

namespace voltaire.Pages
{
    public partial class ContractListTabPage : BaseViewPagerPage
    {
        public ContractListTabPage()
        {
            InitializeComponent();

            search_bar.TextChanged += Search_Bar_TextChanged;
            listview.ItemTapped += Listview_ItemTapped;
            bt_add.Clicked += Bt_Add_Clicked;
        }

        void Handle_Refreshing(object sender, System.EventArgs e)
        {
			var context = BindingContext as ContractPageModel;
			if (context == null)
				return;
            
            context.Init(context.Customer);
            listview.EndRefresh();
        }

        void Listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as ContractModel;
            var context = BindingContext as ContractPageModel;

            context.ItemTapped.Execute(new Tuple<FreshMvvm.IPageModelCoreMethods,Contract>(NavigationService,item.Contract));

            listview.SelectedItem = null;
        }

        void Search_Bar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var context = BindingContext as ContractPageModel;
            if (context == null)
                return;
            
            context.SearchQuery.Execute(null);
        }

        void Bt_Add_Clicked(object sender, EventArgs e)
        {
			var context = BindingContext as ContractPageModel;
			if (context == null)
				return;

            context.AddContract.Execute(NavigationService);
        }
    }
}
