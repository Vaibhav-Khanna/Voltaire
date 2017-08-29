using System;
using voltaire.iOS.Renderers;
using voltaire.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using System.ComponentModel;
using Foundation;

[assembly: ExportRenderer(typeof(CustomSearchBar), typeof(CustomSearchBarRenderer))]
namespace voltaire.iOS.Renderers
{
    public class CustomSearchBarRenderer : SearchBarRenderer
    {
        public CustomSearchBarRenderer()
        {


        }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null && e.NewElement != null)
            {
                var searchbar = Element as SearchBar;

                Control.Layer.CornerRadius = 8;
                Control.ClipsToBounds = true;
                Control.SetShowsCancelButton(false, false);
                Control.Layer.BorderWidth = 1;
                Control.BarTintColor = searchbar.BackgroundColor.ToUIColor();
                Control.Layer.BorderColor = searchbar.BackgroundColor.ToCGColor();
                Control.Layer.ShadowOpacity = 0;

                var uiTextField = (UITextField) ((UISearchBar)Control).ValueForKey(new NSString("_searchField"));	
				uiTextField.BackgroundColor = searchbar.BackgroundColor.ToUIColor();
                uiTextField.TextAlignment = UITextAlignment.Left;

			}
        }
       
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

         
            Control.ShowsCancelButton = false;
        }

    }
}
