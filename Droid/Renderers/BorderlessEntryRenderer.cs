using System;
using Android.Content;
using voltaire.Droid.Renderers;
using voltaire.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(DefaultEntryRenderer))]
[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace voltaire.Droid.Renderers
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        public BorderlessEntryRenderer(Context context) : base(context)
        {

        }

		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                Control.Background = null;
                Control.SetRawInputType(Android.Text.InputTypes.TextFlagNoSuggestions);
            }
		}
	}

    public class DefaultEntryRenderer : EntryRenderer
    {
        public DefaultEntryRenderer(Context context) : base(context)
        {

        }

		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                Control.SetBackgroundResource(Resource.Drawable.Shape);
                Control.SetRawInputType(Android.Text.InputTypes.TextFlagNoSuggestions);
                Control.SetPadding(15, 10, 10, 10);
            }
		}
	}

}
