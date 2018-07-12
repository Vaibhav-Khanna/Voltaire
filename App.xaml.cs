using FreshMvvm;
using voltaire.PageModels;
using voltaire.Pages;
using Xamarin.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using voltaire.Models;
using voltaire.DataStore;
using voltaire.PageModels.Base;
using voltaire.DataStore.Implementation;
using voltaire.DataStore.Abstraction;
using voltaire.Helpers;

namespace voltaire
{
    public partial class App : Application
    {

        public static double ScreenWidth;
        public static double ScreenHeight;

        public App()
        {

            InitializeComponent();

            SQLitePCL.Batteries_V2.Init();
            SQLitePCL.raw.FreezeProvider();

            ProductConstants.Init();

            PclStorage.Init();

            BasePageModel.Init();

            storeManager = DependencyService.Get<IStoreManager>() as StoreManager;

            Init();

            MainPage = new ContentPage();
        }


        public static StoreManager storeManager { get; set; }


        public async void Init()
        {
            if (storeManager == null)
                return;

            if (!storeManager.IsInitialized)
                await storeManager.InitializeAsync();

            //verify Token 
            await storeManager.VerifyTokenAsync();

            var setting = await storeManager.ReadSettingsAsync();

            if (setting == null || string.IsNullOrWhiteSpace(setting.AuthToken))
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    var homePage = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();

                    MainPage = new FreshNavigationContainer(homePage) { BarBackgroundColor = (Color)Resources["turquoiseBlue"], BarTextColor = Color.Black };
                });
            }
            else
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    var homePage = FreshPageModelResolver.ResolvePageModel<HomePageModel>();

                    var homeContainer = new AONNavigationContainer(homePage) { BarBackgroundColor = (Color)Resources["turquoiseBlue"], BarTextColor = Color.White };

                    MainPage = homeContainer;
                });
            }

            if (setting != null && !string.IsNullOrWhiteSpace(setting.AuthToken))
                await storeManager.SyncAllAsync(true);

        }


        protected override void OnStart()
        {
            // Handle when your app starts
#if DEBUG
#else
            AppCenter.Start("ios=3d0ef256-3c90-4860-b789-63ff7e930523;" +
                               "android=c1308239-5175-41b4-a352-61530846726e;",
                               typeof(Analytics), typeof(Crashes), typeof(Distribute));
            Distribute.SetEnabledAsync(true);
#endif
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps

            // Shutdown the database for integrity			
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

    }
}
