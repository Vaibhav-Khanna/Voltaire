using System;
using voltaire.iOS.Renderers;
using voltaire.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(CustomScrollView), typeof(CustomScrollViewRenderer))]
namespace voltaire.iOS.Renderers
{
    public class CustomScrollViewRenderer : ScrollViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);


            {
                if (e.OldElement != null || this.Element == null)
                {
                    return;
                }

                if (e.OldElement != null)
                {
                    e.OldElement.PropertyChanged -= OnElementPropertyChanged;
                }

                e.NewElement.PropertyChanged += OnElementPropertyChanged;
            }
        }
        private void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            this.ShowsVerticalScrollIndicator = false;
        }
    }
}