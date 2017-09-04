using System;
using System.ComponentModel;
using UIKit;
using voltaire.iOS.Renderers;
using voltaire.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderlessPicker), typeof(BorderlessPickerRenderer))]
namespace voltaire.iOS.Renderers
{
    public class BorderlessPickerRenderer : PickerRenderer
    {
       
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            Control.BorderStyle = UITextBorderStyle.None;
            Control.TextAlignment = UITextAlignment.Right;
        }


    }
}
