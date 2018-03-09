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
using Plugin.Permissions;
using Plugin.CrossPlatformTintedImage.Android;

namespace voltaire.Droid
{
    [Activity(Label = "Voltaire", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ScreenOrientation = ScreenOrientation.Landscape, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
          
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            CarouselView.FormsPlugin.Android.CarouselViewRenderer.Init();

            Xamarin.FormsGoogleMaps.Init(this,bundle);           

            Acr.UserDialogs.UserDialogs.Init(this);

            var displayMetrics = this.Resources.DisplayMetrics;

            App.ScreenWidth = ConvertPixelsToDp(displayMetrics.WidthPixels);

            App.ScreenHeight = ConvertPixelsToDp(displayMetrics.HeightPixels);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            TintedImageRenderer.Init();
#if DEBUG
#else
            MobileCenter.Start("c1308239-5175-41b4-a352-61530846726e", typeof(Distribute), typeof(Analytics), typeof(Crashes));
#endif

            LoadApplication(new App());

        }

		private int ConvertPixelsToDp(float pixelValue)
		{
			var dp = (int)((pixelValue) / Resources.DisplayMetrics.Density);
			return dp;
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}
