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

            if (Element != null)
            {
                var element = Element as BorderlessPicker;

                Control.BorderStyle = UITextBorderStyle.None;

                if (element.TextAlignMent == TextAlignment.End)
                    Control.TextAlignment = UITextAlignment.Right;
                else if (element.TextAlignMent == TextAlignment.Start)
                    Control.TextAlignment = UITextAlignment.Left;
                else if (element.TextAlignMent == TextAlignment.Center)
                    Control.TextAlignment = UITextAlignment.Center;
            }

        }


    }
}
