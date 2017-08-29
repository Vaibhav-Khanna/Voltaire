using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Distribute;

using Foundation;
using UIKit;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using CarouselView.FormsPlugin.iOS;

namespace voltaire.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            CarouselViewRenderer.Init();
            Xamarin.FormsGoogleMaps.Init("AIzaSyBAkwMnIRpuTLMTdXgF6-CuYsiYOSFpSpA");

            // Code for starting up the Xamarin Test Cloud Agent
#if DEBUG
            Xamarin.Calabash.Start();
#else
            MobileCenter.Start("3d0ef256-3c90-4860-b789-63ff7e930523",
                               typeof(Distribute), typeof(Analytics), typeof(Crashes));
            Distribute.DontCheckForUpdatesInDebug();
#endif

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
