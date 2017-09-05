using System;
using System.Collections.Generic;
using voltaire.Models;
using voltaire.PageModels;
using Xamarin.Forms;

namespace voltaire.Pages
{
    public partial class NewContractPage : ContentPage
    {
        public NewContractPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            listview.ItemTapped += Listview_ItemTapped; 
        }

        void Listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as AgreementModel;

            var context = BindingContext as NewContractPageModel;

            context.ItemTapped.Execute(item);

            listview.SelectedItem = null;

        }
    }
}
