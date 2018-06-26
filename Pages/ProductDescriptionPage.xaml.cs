using System;
using System.Collections.Generic;
using FreshMvvm;
using Rg.Plugins.Popup.Services;
using voltaire.Converters;
using voltaire.Models;
using voltaire.PageModels;
using voltaire.Pages.Base;
using voltaire.Renderers;
using Xamarin.Forms;

namespace voltaire.Pages
{
    public partial class ProductDescriptionPage : FreshBaseContentPage
    {

        int row_count;
        int row_height = 60;
        bool controlEnabled;

        public ProductDescriptionPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            #region Layout_Tweaks 

            var height = App.ScreenHeight - toolbar.HeightRequest - grid.Margin.Top - grid.Margin.Bottom;

            row_count = (int)height / row_height;

            for (int i = 0; i < row_count; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(row_height, GridUnitType.Absolute) });
            }

            #endregion
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var context = BindingContext as ProductDescriptionPageModel;

            if (context != null)
            {
                controlEnabled = context.IsControlsEnabled;
                InitLayout(context);
            }
        }

        void InitLayout(ProductDescriptionPageModel context)
        {

            int x = 0;
            int y = 0;

            for (int i = 0; i < context.ProductProperties?.Count; i++)
            {

                if (i >= row_count * 2)
                    break;

                if (i < row_count)
                {
                    x = 0;
                    y = i;
                }
                else if (i == row_count)
                {
                    x = 1;
                    y = 0;
                }
                else
                {
                    x = 1;
                    y += 1;
                }

                var item = context.ProductProperties[i];

                if (item.PropertyType == PropertyType.IsBoolean)
                {
                    grid.Children.Add(Switch_Row_Layout(item), x, y);
                }
                else if (item.PropertyType == PropertyType.IsPicker)
                {
                    grid.Children.Add(Picker_Row_Layout(item), x, y);
                }
                else if (item.PropertyType == PropertyType.IsText)
                {
                    grid.Children.Add(Entry_Row_Layout(item), x, y);
                }
                else if (item.PropertyType == PropertyType.IsEditor)
                {
                    grid.Children.Add(Entry_Row_Layout(item), x, y);
                }
                else if(item.PropertyType == PropertyType.IsLabel)
                {
                    grid.Children.Add(Label_Row_Layout(item), x, y);
                }

            }

        }

        StackLayout Switch_Row_Layout(ProductProperty Bind_Context)
        {
            var stack = new StackLayout { BackgroundColor = Color.White, Orientation = StackOrientation.Horizontal, HeightRequest = 60, Margin = 0, HorizontalOptions = LayoutOptions.FillAndExpand, Padding = new Thickness(20, 0, 20, 0) };

            if (Bind_Context == null)
                return stack;

            var label = new Label { FontFamily = "SanFranciscoDisplay-Regular", HorizontalOptions = LayoutOptions.StartAndExpand, VerticalOptions = LayoutOptions.Center, TextColor = (Color)App.Current.Resources["GreyishBrown"] };
            label.SetBinding(Label.TextProperty, "PropertyName");
            label.BindingContext = Bind_Context;

            var sw = new Switch() { IsEnabled = controlEnabled, HorizontalOptions = LayoutOptions.End, VerticalOptions = LayoutOptions.Center };
            sw.SetBinding(Switch.IsToggledProperty, "PropertyValue", BindingMode.TwoWay, converter: new StringToBoolConverter());
            sw.BindingContext = Bind_Context;

            stack.Children.Add(label);
            stack.Children.Add(sw);

            return stack;
        }

        StackLayout Picker_Row_Layout(ProductProperty Bind_Context)
        {
            
            var stack = new StackLayout { BackgroundColor = Color.White, Orientation = StackOrientation.Horizontal, HeightRequest = 60, Margin = 0, HorizontalOptions = LayoutOptions.FillAndExpand, Padding = new Thickness(20, 0, 20, 0) };

            if (Bind_Context == null)
                return stack;

            var label = new Label { FontFamily = "SanFranciscoDisplay-Regular", HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.Center, TextColor = (Color)App.Current.Resources["GreyishBrown"] };
            label.SetBinding(Label.TextProperty, "PropertyName", BindingMode.OneWay);
            label.BindingContext = Bind_Context;

            var picker = new BorderlessPicker() { IsEnabled = controlEnabled, TextAlignMent = TextAlignment.End, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.Center, Title = "Choose", TextColor = (Color)App.Current.Resources["turquoiseBlue"] };
            picker.SetBinding(Picker.ItemsSourceProperty, "ItemSource",BindingMode.TwoWay );
            picker.SetBinding(Picker.SelectedItemProperty, "PropertyValue", BindingMode.TwoWay);

            picker.BindingContext = Bind_Context;

            stack.Children.Add(label);
            stack.Children.Add(picker);

            return stack;
        }

        StackLayout Entry_Row_Layout(ProductProperty Bind_Context)
        {
            var stack = new StackLayout { BackgroundColor = Color.White, Orientation = StackOrientation.Horizontal, HeightRequest = 60, Margin = 0, HorizontalOptions = LayoutOptions.FillAndExpand, Padding = new Thickness(20, 0, 20, 0) };
          
            if (Bind_Context == null)
                return stack;

            Entry entry = null;

            if (Bind_Context.IsNumberKeyboard)
            {
                entry = new NumberEntry(12) { IsEnabled = controlEnabled, HorizontalOptions = LayoutOptions.FillAndExpand, HeightRequest = 40, VerticalOptions = LayoutOptions.Center, TextColor = (Color)App.Current.Resources["GreyishBrown"], FontFamily = "SanFranciscoDisplay-Regular", PlaceholderColor = Color.FromRgb(179, 179, 179) };
                entry.Keyboard = Keyboard.Numeric;             
            }
            else
            {
                entry = new Entry() { IsEnabled = controlEnabled, HorizontalOptions = LayoutOptions.FillAndExpand, HeightRequest = 40, VerticalOptions = LayoutOptions.Center, TextColor = (Color)App.Current.Resources["GreyishBrown"], FontFamily = "SanFranciscoDisplay-Regular", PlaceholderColor = Color.FromRgb(179, 179, 179) };
            }

            entry.SetBinding(Entry.TextProperty, "PropertyValue", BindingMode.TwoWay);
            entry.BindingContext = Bind_Context;

            var label = new Label { FontFamily = "SanFranciscoDisplay-Regular", HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.Center, TextColor = (Color)App.Current.Resources["GreyishBrown"] };
            label.SetBinding(Label.TextProperty, "PropertyName");
            label.BindingContext = Bind_Context;

          
            stack.Children.Add(label);
            stack.Children.Add(entry);

            return stack;
        }

        StackLayout Label_Row_Layout(ProductProperty Bind_Context)
        {
            var stack = new StackLayout { BackgroundColor = Color.White, Orientation = StackOrientation.Horizontal, HeightRequest = 60, Margin = 0, HorizontalOptions = LayoutOptions.FillAndExpand, Padding = new Thickness(20, 0, 20, 0) };

            if (Bind_Context == null)
                return stack;

            var text = new Label { FontFamily = "SanFranciscoDisplay-Regular", HorizontalOptions = LayoutOptions.End, VerticalOptions = LayoutOptions.Center, TextColor = (Color)App.Current.Resources["GreyishBrown"] };
            text.SetBinding(Label.TextProperty, "PropertyValue");
            text.BindingContext = Bind_Context;

            var label = new Label { FontFamily = "SanFranciscoDisplay-Regular", HorizontalOptions = LayoutOptions.StartAndExpand, VerticalOptions = LayoutOptions.Center, TextColor = (Color)App.Current.Resources["GreyishBrown"] };
            label.SetBinding(Label.TextProperty, "PropertyName");
            label.BindingContext = Bind_Context;

            stack.Children.Add(label);
            stack.Children.Add(text);

            stack.SetBinding(IsVisibleProperty, "IsVisible");
            stack.BindingContext = Bind_Context;

            return stack;
        }

         //StackLayout Editor_Row_Layout(ProductProperty Bind_Context)
         //{
         //    var stack = new StackLayout { BackgroundColor = Color.White, Orientation = StackOrientation.Vertical, MinimumHeightRequest = 400, Margin = 0, VerticalOptions = LayoutOptions.FillAndExpand, Padding = new Thickness(20, 0, 20, 0) };

         //    var editor = new ExtendedEditor() { IsEnabled = controlEnabled, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, TextColor = (Color)App.Current.Resources["GreyishBrown"], FontFamily = "SanFranciscoDisplay-Regular", PlaceholderColor = Color.FromRgb(179, 179, 179), BorderColor = Color.FromRgb(179, 179, 179) };
         //    editor.SetBinding(Editor.TextProperty, "PropertyValue", BindingMode.TwoWay);
         //    editor.SetBinding(ExtendedEditor.PlaceholderProperty, "PropertyName");
         //    editor.BindingContext = Bind_Context;

         //    stack.Children.Add(editor);

         //    return stack;
         //} 

        protected override bool OnBackButtonPressed()
        {
            (BindingContext as ProductDescriptionPageModel).BackButton.Execute(null);
            return true;
        }
    }
}
