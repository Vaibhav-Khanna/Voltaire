using System;
using System.Collections.Generic;
using voltaire.PageModels;
using Xamarin.Forms;

namespace voltaire.Pages
{
    public partial class TodoPage 
    {
        public TodoPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            listview.ItemTapped += Listview_ItemTapped;
        }

        void Listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            listview.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            SetMenu(MenuLayout, 3);
                   
        }

        void Handle_Refreshing(object sender, System.EventArgs e)
        {
            (BindingContext as TodoPageModel).Init(null);

            listview.EndRefresh();
        }

    }
}
