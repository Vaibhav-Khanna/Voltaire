using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace voltaire.Pages
{
    public partial class PodiumsPage
    {
        public PodiumsPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetMenu(MenuLayout, 9);

        }
    }
}
