using voltaire.Models;
using voltaire.PageModels;
using Xamarin.Forms;
using System.Linq;
using voltaire.Helpers.Collections;
using Xamarin.Forms.Xaml;

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
            var normal_style = (Style)App.Current.Resources["FilterWeightButtonStyle"];
            bt0.Style = normal_style;
            bt1.Style = normal_style;
            bt2.Style = normal_style;
            bt3.Style = normal_style;
            bt4.Style = normal_style;
            bt5.Style = normal_style;

            (sender as Button).Style = (Style) App.Current.Resources["FilterWeightClickedButtonStyle"];
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            var normal_style = (Style)App.Current.Resources["FilterWeightButtonStyle"];
            bt0.Style = normal_style;
            bt1.Style = normal_style;
            bt2.Style = normal_style;
            bt3.Style = normal_style;
            bt4.Style = normal_style;
            bt5.Style = normal_style;

            var frame_style = (Style)App.Current.Resources["PartnerGradeFrame"];
            var button_style = (Style)App.Current.Resources["PartnerGradeButton"];

            foreach (var item in grades.Children)
            {
                (item as Frame).Style = frame_style;
                ((item as Frame).Content as Button).Style = button_style;
            }

        }

        void Handle_Grade(object sender, System.EventArgs e)
        {
            var frame_style = (Style)App.Current.Resources["PartnerGradeFrame"];
            var button_style = (Style)App.Current.Resources["PartnerGradeButton"];
          
            foreach (var item in grades.Children)
            {
                (item as Frame).Style = frame_style;
                ((item as Frame).Content as Button).Style = button_style;
            }


            ((sender as Button)).Style = (Style)App.Current.Resources["PartnerGradeButtonSelected"];

            ((sender as Button).Parent as Frame).Style = (Style)App.Current.Resources["PartnerGradeFrameSelected"];

            (BindingContext as ContactsPageModel).FilterByGrade.Execute((sender as Button).Text);

        }
    }
}