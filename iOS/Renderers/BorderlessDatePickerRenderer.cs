using System;
using System.ComponentModel;
using UIKit;
using voltaire.iOS.Renderers;
using voltaire.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderlessDatePicker), typeof(BorderlessDatePickerRenderer))]
namespace voltaire.iOS.Renderers
{
    public class BorderlessDatePickerRenderer : DatePickerRenderer
    {
       
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            Control.BorderStyle = UITextBorderStyle.None;
            Control.TextAlignment = UITextAlignment.Left;
        }
       
    }
}
