using System;
using UIKit;
using voltaire.iOS.Renderers;
using Xamarin.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

//[assembly: ExportRenderer(typeof(ContentPage), typeof(ContentPageSwipeRenderer))]
namespace voltaire.iOS.Renderers
{
    public class ContentPageSwipeRenderer : PageRenderer
    {

		public override void ViewWillAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			ViewController.NavigationController.InteractivePopGestureRecognizer.Enabled = true;
			ViewController.NavigationController.InteractivePopGestureRecognizer.Delegate = new UIGestureRecognizerDelegate();
		}

    }
}
