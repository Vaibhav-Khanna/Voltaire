using Foundation;
using UIKit;

using CarouselView.FormsPlugin.iOS;
using Syncfusion.SfPdfViewer.XForms.iOS;
using Syncfusion.ListView.XForms.iOS;
using Lottie.Forms.iOS.Renderers;
using Plugin.CrossPlatformTintedImage.iOS;
using SegmentedControl.FormsPlugin.iOS;
using KeyboardOverlap.Forms.Plugin.iOSUnified;

namespace voltaire.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            var renderer = new Syncfusion.SfAutoComplete.XForms.iOS.SfAutoCompleteRenderer();

            global::Xamarin.Forms.Forms.Init();

            SegmentedControlRenderer.Init();
            CarouselViewRenderer.Init();

            KeyboardOverlapRenderer.Init();
           
             AnimationViewRenderer.Init();

            Xamarin.FormsGoogleMaps.Init("AIzaSyCxXBNtq5ksFXZJBwW_SRkf3gEMOg4YhPc");

            TintedImageRenderer.Init();

            App.ScreenHeight = UIScreen.MainScreen.Bounds.Height;
            App.ScreenWidth = UIScreen.MainScreen.Bounds.Width;

            //BlobCache.ApplicationName = "Voltaire";

            // Code for starting up the Xamarin Test Cloud Agent
#if DEBUG
            Xamarin.Calabash.Start();
#else
            AppCenter.Start("3d0ef256-3c90-4860-b789-63ff7e930523",
                               typeof(Distribute), typeof(Analytics), typeof(Crashes));
            Distribute.DontCheckForUpdatesInDebug();
#endif
            SfListViewRenderer.Init();
            LoadApplication(new App());

            SfPdfDocumentViewRenderer.Init();

            var result = base.FinishedLaunching(app, options);

            UIApplication.SharedApplication.StatusBarHidden = false;

            return result;
        }
    }
}
