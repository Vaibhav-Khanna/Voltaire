using System;
using System.Collections.Generic;
using Xamarin.Forms;
using voltaire.Pages.Base;
using voltaire.PageModels;
using voltaire.Controls;
using System.Linq;

namespace voltaire.Pages
{
    public partial class ContactDetailTabPage : BaseViewPagerPage
    {
        public ContactDetailTabPage()
        {
            InitializeComponent();
            picker_1.TextColor = Color.Transparent;
            picker_2.TextColor = Color.Transparent;
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

            main_context.PropertyChanged += Main_Context_PropertyChanged;

            AddTags(main_context.Tags.ToList());

            main_context.Tags.CollectionChanged += Tags_CollectionChanged;

			if (main_context != null && main_context.CanEdit)   //  Add some extra fields if it is edit page below
			{
				topcontainer.Children.Clear();

				var lb_firstName = new CustomLabelEntry("Name", true) { Keyboard = Keyboard.Text, HorizontalOptions = LayoutOptions.FillAndExpand };
                lb_firstName.SetBinding(CustomLabelEntry.TextProperty, "FirstName", BindingMode.TwoWay);
				lb_firstName.SetBinding(VisualElement.IsEnabledProperty, "CanEdit");
				
				topcontainer.Margin = new Thickness(0, 30, 0, 0);
                topcontainer.Padding = new Thickness(40, 0, 0, 0);
				topcontainer.BackgroundColor = Color.White;
				topcontainer.Children.Add(lb_firstName);
				
                internalNotes.IsVisible = false;              
			}

        }

        void Tags_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (TagControlModel item in e.NewItems)
                {
                    tagContainer.Children.Add(new TagControl() { BindingContext = item });
                }
            }
            else if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach (TagControlModel item in e.OldItems)
                {
                    var view = tagContainer.Children.Where( (arg1) => arg1.BindingContext == item );
                   
                    if(view.Any())
                        tagContainer.Children.Remove(view.FirstOrDefault());
                }
            }
            else if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
            {
                tagContainer.Children.Clear();
            }       
        }

        void AddTags(List<TagControlModel> list)
        {
            tagContainer.Children.Clear();

            foreach (TagControlModel item in list)
            {
                tagContainer.Children.Add(new TagControl() { BindingContext = item });
            }
        }


        void Main_Context_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Tags")
            {
                var main_context = BindingContext as ContactDetailPageModel;
                if (main_context != null)
                {
                    AddTags(main_context.Tags.ToList());                   
                }
            }
        }

		protected override void OnAppearing()
		{
            base.OnAppearing();

            picker_1.TextColor = Color.Transparent;
            picker_2.TextColor = Color.Transparent;
		}

	}
}
