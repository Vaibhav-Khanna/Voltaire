﻿using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Distribute;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;

namespace voltaire.Droid
{
    [Activity(Label = "voltaire.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            CarouselView.FormsPlugin.Android.CarouselViewRenderer.Init();
            Xamarin.FormsGoogleMaps.Init(this,bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
#if DEBUG
#else
            MobileCenter.Start("c1308239-5175-41b4-a352-61530846726e", typeof(Distribute), typeof(Analytics), typeof(Crashes));
#endif

            LoadApplication(new App());
        }
    }
}
