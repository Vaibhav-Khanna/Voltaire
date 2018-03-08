using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using voltaire.Models;
using Xamarin.Forms;

namespace voltaire.PopUps
{
    public partial class AddCustomerPopUp : PopupPage
    {
        public AddCustomerPopUp()
        {
            InitializeComponent();
            CloseWhenBackgroundIsClicked = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            (BindingContext as AddCustomerPopUpModel).SelectedItem = null;
            search.Focus();
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            (BindingContext as AddCustomerPopUpModel).SelectedItem = (Partner)e.Item;
            (BindingContext as AddCustomerPopUpModel).Done.Execute(null);
            list.SelectedItem = null;
        }
    }
}
