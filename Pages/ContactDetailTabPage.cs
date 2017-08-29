using System;
using voltaire.Controls;
using voltaire.Controls.Items;
using voltaire.Converters;
using voltaire.PageModels;
using voltaire.Pages.Base;
using voltaire.Renderers;
using voltaire.Resources;
using Xamarin.Forms;

namespace voltaire.Pages
{
    public class ContactDetailTabPage : BaseViewPagerPage
    {

        StackLayout topcontainer;

        public ContactDetailTabPage()
        {
            InitLayout();
        }


        void InitLayout()
        {

             topcontainer = new StackLayout     //  Contains top horizontal children
            {
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = (Color)App.Current.Resources["White"],
                Padding = new Thickness(40, 20, 30, 20),
                Spacing = 5,
            };


            var Last_visit = new Label     // Last Visit date label
            {
                FontFamily = "Raleway-Light",
                FontSize = 18,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                TextColor = Color.FromRgb(89, 89, 89),
                VerticalOptions = LayoutOptions.Center,
                Text = AppResources.LastVisit + " 12 mai 2016"
            };
            Last_visit.SetBinding(Label.TextProperty, "LastVisit", BindingMode.Default, new DateToStringConverter());

            var Bt_CheckIn = new Button     // Checkin button for checking in
            {
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                Margin = 0,
                Text = AppResources.CheckIn,
                FontSize = 20,
                FontFamily = "Raleway-Regular",
                TextColor = Color.White,
                BackgroundColor = (Color)App.Current.Resources["turquoiseBlue"],
                BorderRadius = 5,
                WidthRequest = 190,
                HeightRequest = 50
            };
            Bt_CheckIn.Clicked += (sender, e) =>
            {
                var context = BindingContext as ContactDetailPageModel;
                context.LastVisit = DateTime.Now;
            };


            var boxview = new RoundedBoxView     // Visual representation button
            {
                HeightRequest = 15,
                WidthRequest = 15,
                Margin = 3,
                Color = (Color)App.Current.Resources["Vermillion"],
                VerticalOptions = LayoutOptions.Center,
            };

            topcontainer.Children.Add(boxview);
            topcontainer.Children.Add(Last_visit);
            topcontainer.Children.Add(Bt_CheckIn);

            var grid = new Grid()
            {
                RowSpacing = 1,
                Margin = new Thickness(40, 0, 0, 0),
                BackgroundColor = (Color)App.Current.Resources["White"],
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start
            };
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(70, GridUnitType.Absolute) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(70, GridUnitType.Absolute) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(70, GridUnitType.Absolute) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(70, GridUnitType.Absolute) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(70, GridUnitType.Absolute) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(70, GridUnitType.Absolute) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(100, GridUnitType.Auto) });


            var weightscale = new WeightScale() { MaxWeight = 5 };   //  Custom Control for marking weight
            weightscale.SetBinding(WeightScale.SelectedScaleProperty, "Weight", BindingMode.TwoWay);
            weightscale.SetBinding(VisualElement.IsEnabledProperty, "CanEdit");
            weightscale.SelectedScaleChanged += () =>
            {

            };
            grid.Children.Add(weightscale, 0, 0);


            var lb_company = new CustomLabelEntry(AppResources.ParentPartner, true) { Keyboard = Keyboard.Text };
     		lb_company.SetBinding(CustomLabelEntry.TextProperty, "CompanyName", BindingMode.TwoWay);
			lb_company.SetBinding(VisualElement.IsEnabledProperty, "CanEdit");
            var lb_phone = new CustomLabelEntry("phone.png") { Keyboard = Keyboard.Numeric,  };
            lb_phone.SetBinding(CustomLabelEntry.TextProperty, "Phone", BindingMode.TwoWay);
            lb_phone.SetBinding(VisualElement.IsEnabledProperty, "CanEdit");
            var lb_location = new CustomLabelEntry("location.png") { Keyboard = Keyboard.Text,  };
            lb_location.SetBinding(CustomLabelEntry.TextProperty, "Address", BindingMode.TwoWay);
            lb_location.SetBinding(VisualElement.IsEnabledProperty, "CanEdit");
            var lb_email = new CustomLabelEntry("email.png") { Keyboard = Keyboard.Email,  };
            lb_email.SetBinding(CustomLabelEntry.TextProperty, "Email", BindingMode.TwoWay);
            lb_email.SetBinding(VisualElement.IsEnabledProperty, "CanEdit");
            var lb_website = new CustomLabelEntry("www.png") { Keyboard = Keyboard.Url,  };
            lb_website.SetBinding(CustomLabelEntry.TextProperty, "Website", BindingMode.TwoWay);
            lb_website.SetBinding(VisualElement.IsEnabledProperty, "CanEdit");


            var lb_tags = new Label
            {
                FontFamily = "Raleway-Medium",
                TextColor = (Color)App.Current.Resources["GreyishBrown"],
                FontSize = 24,
                Text = AppResources.Tags,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Start
            };

            var bt_addtags = new Button
            {
                FontFamily = "Raleway-Regular",
                FontSize = 16,
                TextColor = (Color)App.Current.Resources["turquoiseBlue"],
                Text = AppResources.addtags,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand
            };

            grid.Children.Add(lb_company,0,1);
            grid.Children.Add(lb_phone, 0, 2);
            grid.Children.Add(lb_location, 0, 3);
            grid.Children.Add(lb_email, 0, 4);
            grid.Children.Add(lb_website, 0, 5);
            grid.Children.Add(new StackLayout { Children = { lb_tags, bt_addtags }, Spacing = 20, Orientation = StackOrientation.Vertical, BackgroundColor = Color.White, Padding = new Thickness(0, 30, 0, 40), HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, }, 0, 6);

            var scrollview = new ScrollView
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = ScrollOrientation.Vertical,
                Content = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    BackgroundColor = Color.White,
                    Padding = 0,
                    Margin = 0,
                    Children = { topcontainer, grid },
                    Spacing = 0
                },
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            Content = scrollview;

        }

        protected override void BindingContextSet()   // Check if its editing page, Then do some modifications
        {
            base.BindingContextSet();

            var context = BindingContext as TTab;

            if (context == null)
                return;

            var main_context = context.ViewBindingContext as ContactDetailPageModel;

            if(main_context!= null && main_context.CanEdit)   //  Add some extra fields for editing below
            {
                topcontainer.Children.Clear();

                var lb_firstName = new CustomLabelEntry("First Name", true) { Keyboard = Keyboard.Text,HorizontalOptions = LayoutOptions.FillAndExpand };
                lb_firstName.SetBinding(CustomLabelEntry.TextProperty, "FirstName", BindingMode.TwoWay);
                lb_firstName.SetBinding(VisualElement.IsEnabledProperty, "CanEdit");
                var lb_lastName = new CustomLabelEntry("Last Name", true) { Keyboard = Keyboard.Text,HorizontalOptions = LayoutOptions.FillAndExpand };
				lb_lastName.SetBinding(CustomLabelEntry.TextProperty, "LastName", BindingMode.TwoWay);
				lb_lastName.SetBinding(VisualElement.IsEnabledProperty, "CanEdit");

                topcontainer.Margin = new Thickness(0, 70, 0, 20);
                topcontainer.BackgroundColor = Color.White;
                topcontainer.Children.Add(lb_firstName);
                topcontainer.Children.Add(lb_lastName);

            }

        }

    }
}

