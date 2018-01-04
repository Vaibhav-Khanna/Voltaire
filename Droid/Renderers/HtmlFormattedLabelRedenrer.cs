using Android.Text;
using Android.Widget;
using voltaire.Droid.Renderers;
using voltaire.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(HtmlFormattedLabel), typeof(HtmlFormattedLabelRenderer))]
namespace voltaire.Droid.Renderers
{
    public class HtmlFormattedLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            var view = (HtmlFormattedLabel)Element;
            if (view == null) return;

            Control.SetText(Html.FromHtml(view.Text.ToString(), FromHtmlOptions.ModeLegacy), TextView.BufferType.Spannable);
        }
    }
}
