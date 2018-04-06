using System;
using System.Collections.Generic;
using System.Linq;
using voltaire.Controls;
using voltaire.PageModels;
using voltaire.Pages.Base;
using Xamarin.Forms;

namespace voltaire.Pages
{
    public partial class ContactAddPage : BasePage
    {
        public ContactAddPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            entry.FocusChanged += Handle_Focused;
           
        }

        void Handle_SelectedScaleChanged()
        {
            
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var main_context = BindingContext as ContactAddPageModel;

            if (main_context == null)
                return;

            main_context.PropertyChanged += Main_Context_PropertyChanged;

            main_context.Tags.CollectionChanged += Tags_CollectionChanged;
        }


        void Tags_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (TagControlModel item in e.NewItems)
                {
                    tagContainer.Children.Add(new TagControl() { BindingContext = item });
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach (TagControlModel item in e.OldItems)
                {
                    var view = tagContainer.Children.Where((arg1) => arg1.BindingContext == item);

                    if (view.Any())
                        tagContainer.Children.Remove(view.FirstOrDefault());
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
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
                var main_context = BindingContext as ContactAddPageModel;
                if (main_context != null)
                {
                    AddTags(main_context.Tags.ToList());
                }
            }
        }


        async void Handle_Focused()
        {
            entry.Unfocus();
           (BindingContext as ContactAddPageModel).AddCustomer.Execute(null);
            await System.Threading.Tasks.Task.Delay(100);
            entry.Unfocus();
        }


		protected override bool OnBackButtonPressed()
		{
            (BindingContext as ContactAddPageModel).BackCommand.Execute(null);
            return true;
		}
	}
}
