using System;
using System.ComponentModel;
using voltaire.iOS.Renderers;
using voltaire.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CoreGraphics;

[assembly: ExportRenderer(typeof(CustomProgressBar), typeof(CustomProgressBarRenderer))]
namespace voltaire.iOS.Renderers
{
    public class CustomProgressBarRenderer : ProgressBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ProgressBar> e)
        {
            base.OnElementChanged(e);

            var newElement = e.NewElement as CustomProgressBar;

            Control.TrackTintColor = Color.FromRgb(216, 216, 216).ToUIColor();

            if (newElement == null)
                return;

            if (newElement.ProgressColor == "Blue")
                Control.ProgressTintColor = Color.FromRgb(0, 222, 255).ToUIColor();// If you want to change the color

            if (newElement.ProgressColor == "Orange")
                Control.ProgressTintColor = Color.FromRgb(255, 168, 0).ToUIColor();// If you want to change the color

            if (newElement.ProgressColor == "Green")
                Control.ProgressTintColor = Color.FromRgb(139, 226, 27).ToUIColor();// If you want to change the color

            if (newElement.ProgressColor == "Red")
                Control.ProgressTintColor = Color.FromRgb(255, 103, 103).ToUIColor();// If you want to change the color

        }


        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            var X = 1.0f;
            var Y = 5.0f; //This changes the height

            CGAffineTransform transform = CGAffineTransform.MakeScale(X, Y);
            this.Transform = transform;
        }
    }
}