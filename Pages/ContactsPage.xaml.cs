﻿using Xamarin.Forms;

namespace voltaire.Pages
{
    public partial class ContactsPage
    {
        public ContactsPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetMenu(MenuLayout, 1);

        }

    }
}