using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace voltaire.PopUps
{
    public partial class SearchStateCountryPopUp : PopupPage
    {
        public SearchStateCountryPopUp()
        {
            InitializeComponent();
            CloseWhenBackgroundIsClicked = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            (BindingContext as SearchStateCountryPopUpModel).SelectedItem = null;
            search.Focus();
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            (BindingContext as SearchStateCountryPopUpModel).SelectedItem = e.Item;
            (BindingContext as SearchStateCountryPopUpModel).Done.Execute(null);
            list.SelectedItem = null;
        }

        protected override bool OnBackButtonPressed()
        {
            (BindingContext as SearchStateCountryPopUpModel).Close.Execute(null);
            return true;
        }
    }
}
