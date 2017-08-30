using Xamarin.Forms;

namespace voltaire.Pages
{
    public partial class GoalsPage
    {


        public GoalsPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetMenu(MenuLayout, 8);

        }

    }
}
