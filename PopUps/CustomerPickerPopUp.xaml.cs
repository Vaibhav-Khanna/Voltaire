using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace voltaire.PopUps
{
    public partial class CustomerPickerPopUp : PopupPage
    {
        public CustomerPickerPopUp()
        {
            InitializeComponent();

			Padding = new Thickness(150, 150, 150, 250);
			CloseWhenBackgroundIsClicked = false;

			listview.ItemTapped += Listview_ItemTapped;
        }

		void Listview_ItemTapped(object sender, ItemTappedEventArgs e)
		{
            var context = BindingContext as CustomerPickerPopupModel;

			if (context != null)
				context.ItemSelected.Execute(listview.SelectedItem);

			listview.SelectedItem = null;
		}

        protected override bool OnBackButtonPressed()
        {
            (BindingContext as CustomerPickerPopupModel).Close.Execute(null);
            return true;
        }
    }
}
