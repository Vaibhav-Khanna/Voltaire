using FreshMvvm;
using voltaire.PageModels;
using voltaire.Pages;
using Xamarin.Forms;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using Microsoft.Azure.Mobile.Distribute;

namespace voltaire
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var homePage = FreshPageModelResolver.ResolvePageModel<HomePageModel>();

            var homeContainer = new FreshNavigationContainer(homePage) { BarTextColor = Color.White };
            MainPage = homeContainer;

        }

        protected override void OnStart()
        {
            // Handle when your app starts
#if DEBUG
#else
            MobileCenter.Start("ios=3d0ef256-3c90-4860-b789-63ff7e930523;" +
                               "android=c1308239-5175-41b4-a352-61530846726e;",
                               typeof(Analytics), typeof(Crashes), typeof(Distribute));
            Distribute.SetEnabledAsync(true);
#endif
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
