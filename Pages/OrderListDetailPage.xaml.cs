using System;
using System.Collections.Generic;
using Xamarin.Forms;
using voltaire.Pages.Base;
using voltaire.PageModels;

namespace voltaire.Pages
{
    public partial class OrderListDetailPage : BasePage
    {
        public OrderListDetailPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

		void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			var item = e.Item as ProductQuotationModel;

            (BindingContext as OrderListDetailPageModel).itemTapped.Execute(item);

			listview.SelectedItem = null;
		}


    }
}
