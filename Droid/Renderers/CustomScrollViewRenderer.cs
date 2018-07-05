using System.ComponentModel;
using Android.Content;
using voltaire.Droid.Renderers;
using voltaire.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

//[assembly: ExportRenderer(typeof(ScrollView), typeof(CustomScrollViewRenderer))]
namespace voltaire.Droid.Renderers
{
    public class CustomScrollViewRenderer : ScrollViewRenderer
    {

        public CustomScrollViewRenderer(Context context) : base(context)
        {

        }


        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || this.Element == null)
            {
                return;
            }

            if (e.OldElement != null)
            {
                
                e.OldElement.PropertyChanged -= OnElementPropetyChanged;
            }

            if(e.NewElement!=null)
            e.NewElement.PropertyChanged += OnElementPropetyChanged;
        }

        private void OnElementPropetyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (ChildCount > 0 && this.Element != null)
            {
                GetChildAt(0).HorizontalScrollBarEnabled = false;
                GetChildAt(0).VerticalScrollBarEnabled = false;
            }
        }
    }
}