using System;
using System.ComponentModel;
using UIKit;
using voltaire.iOS.Renderers;
using voltaire.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderlessTimePicker), typeof(BorderlessTimePickerRenderer))]
namespace voltaire.iOS.Renderers
{
    public class BorderlessTimePickerRenderer : TimePickerRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);

            Control.BorderStyle = UITextBorderStyle.None;
            Control.Layer.BorderWidth = 0;

        }

    }
}
