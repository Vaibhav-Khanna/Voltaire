using System;
using System.Collections.Generic;
using voltaire.Pages.Base;
using Xamarin.Forms;
using System.Linq;
using voltaire.Models;
using Syncfusion.ListView.XForms;

namespace voltaire.Pages
{
    public partial class MessagesPage
    {
        
        public MessagesPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            listview.ItemTapped += Listview_ItemTapped;
        }


        void Listview_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var item = e.ItemData as MessageModel;

            item.Expanded = item.Expanded ? false : true;

            listview.SelectedItem = null;
        }

       
    }
}
