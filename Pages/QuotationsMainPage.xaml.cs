using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using voltaire.Models;
using voltaire.PageModels;
using voltaire.Pages.Base;
using Xamarin.Forms;


namespace voltaire.Pages
{
    public partial class QuotationsMainPage : BasePage
    {
        public QuotationsMainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            listview.ItemTapped += Listview_ItemTapped;
        }

        void Listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var context = BindingContext as QuotationsMainPageModel;
            context.TapQuotation.Execute( (QuotationsModel) e.Item);
            listview.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetMenu(MenuLayout, 6);
        }

        void Handle_Refreshing(object sender, System.EventArgs e)
        {
            var context = BindingContext as QuotationsMainPageModel;

            if (context == null)
                return;

            //context.Init(context.Customer);

            listview.EndRefresh();
        }

    }
}
