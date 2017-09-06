using System;
using System.Collections.Generic;
using voltaire.PageModels;
using Xamarin.Forms;

namespace voltaire.Pages
{
    public partial class ContactDetailPage : FreshMvvm.FreshBaseContentPage
    {
        public ContactDetailPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();           
        }

       
		protected override void OnBindingContextChanged()    // Check if its an editing page, Then do some modifications
		{
			base.OnBindingContextChanged();

			var context = BindingContext as ContactDetailPageModel;

			if (context != null)
			{
				if (context.CanEdit)
				{
                    tabslider.IsVisible = false;
                    Grid.SetRow(viewpager,1);
                    Grid.SetRowSpan(viewpager,2);
					viewpager.IsSwipingEnabled = false;
                    ImageBack.IsVisible = false;
                    LabelBack.IsVisible = true;
                }
				else
				{
					tabslider.IsVisible = true;
					viewpager.IsSwipingEnabled = true;
				}
			}

		}

		protected override void OnAppearing()   //  call the OnAppearing method of tabslider
		{
			base.OnAppearing();
			tabslider.ViewHasAppeared();
		}


	}
}
