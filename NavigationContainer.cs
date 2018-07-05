using System;
using FreshMvvm;
using Xamarin.Forms;

namespace voltaire
{
    public class AONNavigationContainer : FreshNavigationContainer
    {
        public AONNavigationContainer(Page page) : base(page)
        {
        }

        protected override Page CreateContainerPage(Page page)
        {
            if (page is NavigationPage || page is MasterDetailPage || page is TabbedPage)
                return page;

            return new AONNavigationPage(page);
        }
    }

    public class AONNavigationPage : NavigationPage
    {
        public AONNavigationPage()
        {
            
        } 

        public AONNavigationPage(Page page) : base(page)
        {
            
            BarTextColor = Color.White;
        }
    }
}
