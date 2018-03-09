using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Util;
using voltaire.Droid.Renderers;
using voltaire.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(RoundedBoxView), typeof(RoundedBoxViewRenderer))]
[assembly: ExportRenderer(typeof(BorderlessTimePicker), typeof(BorderlessTimePickerRenderer))]
[assembly: ExportRenderer(typeof(BorderlessPicker), typeof(BorderlessPickerRenderer))]
namespace voltaire.Droid.Renderers
{
    public class BorderlessPickerRenderer : PickerRenderer
    {
     
        public BorderlessPickerRenderer(Context context) : base(context)
        {

        }

		protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
		{
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                Control.Background = null;

                Control.SetRawInputType(Android.Text.InputTypes.TextFlagNoSuggestions);


                var element = Element as BorderlessPicker;

                if (element.TextAlignMent == Xamarin.Forms.TextAlignment.End)
                    Control.Gravity = Android.Views.GravityFlags.End;
                else if (element.TextAlignMent == Xamarin.Forms.TextAlignment.Start)
                    Control.Gravity = Android.Views.GravityFlags.Start;
                else if (element.TextAlignMent == Xamarin.Forms.TextAlignment.Center)
                    Control.Gravity = Android.Views.GravityFlags.Center;
            }
		}

	}

    public class BorderlessTimePickerRenderer : TimePickerRenderer
    {
        public BorderlessTimePickerRenderer(Context context) : base(context)
        {

        }

		protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
		{
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                Control.Background = null;
                Control.SetRawInputType(Android.Text.InputTypes.TextFlagNoSuggestions);
            }
		}
	}


    public class DefaultPageRenderer : PageRenderer
    {
        public DefaultPageRenderer(Context context) : base(context)
        {
           
        }

		protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
		{
            base.OnElementChanged(e);
		}

	}


    public class RoundedBoxViewRenderer : BoxRenderer
    {

        public RoundedBoxViewRenderer(Context context) : base(context)
        {

        }


        private float _cornerRadius;  
        private RectF _bounds;  
        private Path _path;  

        protected override void OnElementChanged(ElementChangedEventArgs <BoxView> e) {  
            base.OnElementChanged(e);  
            if (Element == null) {  
                return;  
            }  
            var element = (RoundedBoxView) Element;  
            _cornerRadius = TypedValue.ApplyDimension(ComplexUnitType.Dip, (float) element.HeightRequest/2, Context.Resources.DisplayMetrics);  
        }  

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh) {  
            base.OnSizeChanged(w, h, oldw, oldh);  
            if (w != oldw && h != oldh) {  
                _bounds = new RectF(0, 0, w, h);  
            }  
            _path = new Path();  
            _path.Reset();  
            _path.AddRoundRect(_bounds, _cornerRadius, _cornerRadius, Path.Direction.Cw);  
            _path.Close();  
        }  

        public override void Draw(Canvas canvas) {  
            canvas.Save();  
            canvas.ClipPath(_path);  
            base.Draw(canvas);  
            canvas.Restore();  
        }  

	}
}
