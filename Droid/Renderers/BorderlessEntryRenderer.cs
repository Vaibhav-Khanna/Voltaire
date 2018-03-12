using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics;
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
                changeFont();
            }
		}

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Label.FontFamilyProperty.PropertyName)
            {
                changeFont();
            }
        }

        void changeFont()
        {
            try
            {
                if (Element.FontFamily == "Raleway-Regular")
                {
                    Typeface font = Typeface.CreateFromAsset(Context.Assets, "fonts/Raleway-Regular.ttf");  // font name specified here
                    Control.Typeface = font;
                }
                else if (Element.FontFamily == "Raleway-Heavy")
                {
                    Typeface font = Typeface.CreateFromAsset(Context.Assets, "fonts/Raleway-Heavy.ttf");  // font name specified here
                    Control.Typeface = font;
                }
                else if (Element.FontFamily == "Raleway-SemiBold")
                {
                    Typeface font = Typeface.CreateFromAsset(Context.Assets, "fonts/Raleway-SemiBold.ttf");  // font name specified here
                    Control.Typeface = font;
                }
                else if (Element.FontFamily == "Raleway-Bold")
                {
                    Typeface font = Typeface.CreateFromAsset(Context.Assets, "fonts/Raleway-Bold.ttf");  // font name specified here
                    Control.Typeface = font;
                }
                else if (Element.FontFamily == "Raleway-Medium")
                {
                    Typeface font = Typeface.CreateFromAsset(Context.Assets, "fonts/Raleway-Medium.ttf");  // font name specified here
                    Control.Typeface = font;
                }
                else if (Element.FontFamily == "SanFranciscoDisplay-Light")
                {
                    Typeface font = Typeface.CreateFromAsset(Context.Assets, "fonts/SanFranciscoDisplay-Light.otf");  // font name specified here
                    Control.Typeface = font;
                }
                else if (Element.FontFamily == "SanFranciscoDisplay-Regular")
                {
                    Typeface font = Typeface.CreateFromAsset(Context.Assets, "fonts/SanFranciscoDisplay-Regular.otf");  // font name specified here
                    Control.Typeface = font;
                }
                else if (Element.FontFamily == "SanFranciscoDisplay-Semibold")
                {
                    Typeface font = Typeface.CreateFromAsset(Context.Assets, "fonts/SanFranciscoDisplay-Semibold.otf");  // font name specified here
                    Control.Typeface = font;
                }
            }catch(Exception ex)
            {
                
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
                changeFont();              
            }
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
            base.OnElementPropertyChanged(sender, e);

            if(e.PropertyName == Label.FontFamilyProperty.PropertyName)
            {

                changeFont();

            }
		}

        void changeFont()
        {
            try
            {
                if (Element.FontFamily == "Raleway-Regular")
                {
                    Typeface font = Typeface.CreateFromAsset(Context.Assets, "fonts/Raleway-Regular.ttf");  // font name specified here
                    Control.Typeface = font;
                }
                else if (Element.FontFamily == "Raleway-Heavy")
                {
                    Typeface font = Typeface.CreateFromAsset(Context.Assets, "fonts/Raleway-Heavy.ttf");  // font name specified here
                    Control.Typeface = font;
                }
                else if (Element.FontFamily == "Raleway-SemiBold")
                {
                    Typeface font = Typeface.CreateFromAsset(Context.Assets, "fonts/Raleway-SemiBold.ttf");  // font name specified here
                    Control.Typeface = font;
                }
                else if (Element.FontFamily == "Raleway-Bold")
                {
                    Typeface font = Typeface.CreateFromAsset(Context.Assets, "fonts/Raleway-Bold.ttf");  // font name specified here
                    Control.Typeface = font;
                }
                else if (Element.FontFamily == "Raleway-Medium")
                {
                    Typeface font = Typeface.CreateFromAsset(Context.Assets, "fonts/Raleway-Medium.ttf");  // font name specified here
                    Control.Typeface = font;
                }
                else if (Element.FontFamily == "SanFranciscoDisplay-Light")
                {
                    Typeface font = Typeface.CreateFromAsset(Context.Assets, "fonts/SanFranciscoDisplay-Light.otf");  // font name specified here
                    Control.Typeface = font;
                }
                else if (Element.FontFamily == "SanFranciscoDisplay-Regular")
                {
                    Typeface font = Typeface.CreateFromAsset(Context.Assets, "fonts/SanFranciscoDisplay-Regular.otf");  // font name specified here
                    Control.Typeface = font;
                }
                else if (Element.FontFamily == "SanFranciscoDisplay-Semibold")
                {
                    Typeface font = Typeface.CreateFromAsset(Context.Assets, "fonts/SanFranciscoDisplay-Semibold.otf");  // font name specified here
                    Control.Typeface = font;
                }
            }
            catch(Exception ex)
            {
                
            }
        }
	}

}
