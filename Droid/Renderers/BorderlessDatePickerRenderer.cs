using System;
using Android.Content;
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
                Control.SetPadding(Control.PaddingLeft, 0, Control.PaddingRight, 0);
            }
		}
	}

}
