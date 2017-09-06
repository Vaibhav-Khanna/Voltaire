using System;
using System.Collections.Generic;
using Xamarin.Forms;
using voltaire.Pages.Base;
using voltaire.PageModels;
using voltaire.Controls;

namespace voltaire.Pages
{
    public partial class ContactDetailTabPage : BaseViewPagerPage
    {
        public ContactDetailTabPage()
        {
            InitializeComponent();
        }

        void Handle_SelectedScaleChanged()
        {
            
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var main_context = BindingContext as ContactDetailPageModel;

            if (main_context == null)
                return;

			if (main_context != null && main_context.CanEdit)   //  Add some extra fields if it is edit page below
			{
				topcontainer.Children.Clear();

				var lb_firstName = new CustomLabelEntry("First Name", true) { Keyboard = Keyboard.Text, HorizontalOptions = LayoutOptions.FillAndExpand };
				lb_firstName.SetBinding(CustomLabelEntry.TextProperty, "FirstName", BindingMode.TwoWay);
				lb_firstName.SetBinding(VisualElement.IsEnabledProperty, "CanEdit");
				var lb_lastName = new CustomLabelEntry("Last Name", true) { Keyboard = Keyboard.Text, HorizontalOptions = LayoutOptions.FillAndExpand };
				lb_lastName.SetBinding(CustomLabelEntry.TextProperty, "LastName", BindingMode.TwoWay);
				lb_lastName.SetBinding(VisualElement.IsEnabledProperty, "CanEdit");

				topcontainer.Margin = new Thickness(0, 30, 0, 20);
				topcontainer.BackgroundColor = Color.White;
				topcontainer.Children.Add(lb_firstName);
				topcontainer.Children.Add(lb_lastName);
			}

        }
       

    }
}
