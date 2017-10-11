using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace voltaire.PopUps
{
    
    public partial class AddTagsPopUp : PopupPage
    {
        public AddTagsPopUp()
        {
            InitializeComponent();

            CloseWhenBackgroundIsClicked = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            (BindingContext as AddTagsPopUpModel).SelectedItem = null;
        }
    }
}
