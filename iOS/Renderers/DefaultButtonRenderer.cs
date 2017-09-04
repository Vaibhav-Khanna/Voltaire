using System;
using voltaire.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Button),typeof(DefaultButtonRenderer))]
namespace voltaire.iOS.Renderers
{
    public class DefaultButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if(Control!=null)
            {
                Control.ImageEdgeInsets = new UIKit.UIEdgeInsets(25, -10, 25,0);
            }

        }
    }
}
