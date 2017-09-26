using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using voltaire.Models;
using voltaire.PageModels;
using voltaire.Pages.Base;
using Xamarin.Forms;

namespace voltaire.Pages
{
    public partial class ContractsMainPage : BasePage
    {
        public ContractsMainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            listview.ItemTapped += Listview_ItemTapped;
        }

		protected override void OnAppearing()
		{
			base.OnAppearing();
			SetMenu(MenuLayout, 7);
		}


        async void Handle_Refreshing(object sender, System.EventArgs e)
        {
            await Task.Delay(2000);
            listview.EndRefresh();
        }


        void Listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var context = BindingContext as ContractsMainPageModel;

            context.ItemTapped.Execute((e.Item as ContractModel).Contract);

            listview.SelectedItem = null;
        }
    }
}
