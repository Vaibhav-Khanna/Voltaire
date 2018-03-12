using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics;
using voltaire.Droid.Renderers;
using voltaire.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(Button), typeof(DefaultButtonRenderer))]
[assembly: ExportRenderer(typeof(Editor), typeof(BorderlessEditorRenderer))]
[assembly: ExportRenderer(typeof(SearchBar), typeof(BorderlessSearchBar))]
[assembly: ExportRenderer(typeof(BorderlessDatePicker), typeof(BorderlessDatePickerRenderer))]
namespace voltaire.Droid.Renderers
{
    public class BorderlessDatePickerRenderer : DatePickerRenderer
    {
        public BorderlessDatePickerRenderer(Context context) : base(context)
        {
           
        }


		protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
		{
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                Control.Background = null;
                Control.SetRawInputType(Android.Text.InputTypes.TextFlagNoSuggestions);
                Control.TextAlignment = Android.Views.TextAlignment.ViewStart;
            }
		}

		
		
	}


    public class BorderlessSearchBar : SearchBarRenderer
    {

        public BorderlessSearchBar(Context context) : base(context)
        {

        }

		protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
		{
            base.OnElementChanged(e);

            int searchPlateId = Control.Context.Resources.GetIdentifier("android:id/search_plate", null, null);
           
            Android.Views.View searchPlate = Control.FindViewById(searchPlateId);

            Control.SetInputType(Android.Text.InputTypes.TextFlagNoSuggestions);

            if (searchPlate != null)
            {
                searchPlate.SetBackgroundColor(Xamarin.Forms.Color.Transparent.ToAndroid());
            }
		}

       


	}

    public class BorderlessEditorRenderer : EditorRenderer
    {
        public BorderlessEditorRenderer(Context context) : base(context)
        {

        }

		protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
		{
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                Control.Background = null;
                Control.SetRawInputType(Android.Text.InputTypes.TextFlagNoSuggestions);
            }
		}
	}

    public class DefaultButtonRenderer : Xamarin.Forms.Platform.Android.AppCompat.ButtonRenderer
    {
        public DefaultButtonRenderer(Context context) : base(context)
        {

        }

		protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                Control.SetAllCaps(false);
                Control.SetPadding(Control.PaddingLeft, 5, Control.PaddingRight, 5);
                Control.Elevation = 0;
                Control.SetShadowLayer(0, 0, 0, Android.Graphics.Color.Transparent);
                changeFont();
            }
		}

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Button.FontFamilyProperty.PropertyName)
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
