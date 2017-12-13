using voltaire.Models;
using voltaire.PageModels;
using Xamarin.Forms;
using System.Linq;
using voltaire.Helpers.Collections;

namespace voltaire.Pages
{
    public partial class ContactsPage
    {

        ContactsPageModel context;

        public ContactsPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetMenu(MenuLayout, 1);
            context = BindingContext as ContactsPageModel;
        }

        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue) && !string.IsNullOrWhiteSpace(e.OldTextValue))
            {               
                Device.BeginInvokeOnMainThread(() =>
               {
                   searchBar.Unfocus();
               });
            }
        }

        void Handle_ItemAppearing(object sender, Xamarin.Forms.ItemVisibilityEventArgs e)
        {          
            if( (e.Item as CustomerModel)?.Customer == context.Customers.LastOrDefault())
            {
                context.LoadMore.Execute(null);
            }
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            
        }
    }
}