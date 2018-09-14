using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using voltaire.Models;
using voltaire.PageModels;
using voltaire.Pages.Base;
using Xamarin.Forms;


namespace voltaire.Pages
{
    public partial class QuotationsMainPage : BasePage
    {

        QuotationsMainPageModel Context;

        public QuotationsMainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
           
            InitializeComponent();
            listview.ItemTapped += Listview_ItemTapped;
        }

        void Listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var context = BindingContext as QuotationsMainPageModel;
            context.TapQuotation.Execute( (QuotationsModel) e.Item);
            listview.SelectedItem = null;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if(BindingContext is QuotationsMainPageModel)
            {
                Context = (BindingContext as QuotationsMainPageModel);

                Context.PropertyChanged -= Context_PropertyChanged;
                Context.PropertyChanged += Context_PropertyChanged;
            }
        }

        void Context_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "IsLoading")
            {
                SegControl.IsVisible = !Context.IsLoading;
            }

            if (e.PropertyName == "IsRefreshing")
            {
                SegControl.IsVisible = !Context.IsRefreshing;
            }
        }


        void Handle_ItemAppearing(object sender, Xamarin.Forms.ItemVisibilityEventArgs e)
        {
            if ( (e.Item as QuotationsModel) == Context.QuotationsItemSource.LastOrDefault())
            {
                Context.LoadMore.Execute(null);
            }
        }

        void Handle_ValueChanged(object sender, SegmentedControl.FormsPlugin.Abstractions.ValueChangedEventArgs e)
        {
            Context.SelectedSegment = e.NewValue;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetMenu(MenuLayout, 6);
        }

    }
}
