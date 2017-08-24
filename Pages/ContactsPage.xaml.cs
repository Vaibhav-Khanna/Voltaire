using voltaire.PageModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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