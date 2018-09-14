using System.Collections.Generic;
using System.Linq;
using FreshMvvm;
using voltaire.Controls.Items;
using voltaire.Models;
using voltaire.PageModels;
using voltaire.Resources;
using Xamarin.Forms;

namespace voltaire.Pages.Base
{
    public partial class BasePage : BaseDisposePage
    {
        private MenuLeftItem _selectedMenuItem;
        private List<MenuLeftItem> _pageIcons;
        private Page _selectedPage;

        public BasePage()
        {
            NavigationPage.SetBackButtonTitle(this,AppResources.Back);
            InitializeComponent();
        }

        protected void SetMenu(StackLayout view, int selectedIndex)
        {
            if (_pageIcons != null && _pageIcons.Any()) return;

            _pageIcons = new List<MenuLeftItem>
            {
                new MenuLeftItem {Title = "",  IsEnabled = true, opacity=1, IsSelected = false, IconSource = "home"},
                new MenuLeftItem {Title = "",  IsEnabled = true, opacity=1, IsSelected = false, IconSource = "contact"},
                new MenuLeftItem {Title = "", IsEnabled = true, opacity = 1, IsSelected = false, IconSource = "map"},
                new MenuLeftItem {Title = "", IsEnabled = false, opacity = 0.5, IsSelected = false, IconSource = "todo"},
                new MenuLeftItem {Title = "", IsEnabled = false, opacity = 0.5,IsSelected = false, IconSource = "agenda"},
                new MenuLeftItem {Title = "", IsEnabled = false, opacity = 0.5,IsSelected = false, IconSource = "report"},
                new MenuLeftItem {Title = "", IsEnabled = true, opacity = 1, IsSelected = false, IconSource = "quotation"},
                new MenuLeftItem {Title = "", IsEnabled = false, opacity = 0.5,IsSelected = false, IconSource = "contract"},
                new MenuLeftItem {Title = "", IsEnabled = false, opacity = 0.5,IsSelected = false, IconSource = "goals"},
                new MenuLeftItem {Title = "", IsEnabled = false, opacity = 0.5, IsSelected = false, IconSource = "podiums"},
            };

            _pageIcons[selectedIndex].IsSelected = true;
            _selectedMenuItem = _pageIcons[selectedIndex];

            foreach (var item in _pageIcons)
            {
                var cell = new LeftMenuItem();
                cell.BindingContext = item;
                cell.ItemClicked += ItemClicked;
                view.Children.Add(cell);
            }
        }

        protected async void changeCurrentView(string pageName)
        {
            // change view
            Page page = null;

            switch (pageName)
            {
                case "home":
                    page = FreshPageModelResolver.ResolvePageModel<HomePageModel>();
                    break;
                case "contact":
                    page = FreshPageModelResolver.ResolvePageModel<ContactsPageModel>();
                    break;
                case "map":
                    page = FreshPageModelResolver.ResolvePageModel<MapMainPageModel>();
                    break;
                case "todo":
                    page = FreshPageModelResolver.ResolvePageModel<TodoPageModel>();
                    break;
                case "agenda":
                    page = FreshPageModelResolver.ResolvePageModel<AgendaPageModel>();
                    break;
                case "report":
                    page = FreshPageModelResolver.ResolvePageModel<ReportsPageModel>();
                    break;
                case "quotation":
                    page = FreshPageModelResolver.ResolvePageModel<QuotationsMainPageModel>();
                    break;
                case "contract":
                    page = FreshPageModelResolver.ResolvePageModel<ContractsMainPageModel>();
                    break;
                case "goals":
                    page = FreshPageModelResolver.ResolvePageModel<GoalsPageModel>();
                    break;
                case "podiums":
                    page = FreshPageModelResolver.ResolvePageModel<PodiumsPageModel>();
                    break;

            }

            if (page != null)
            {

                if (_selectedPage != null && Navigation.NavigationStack.Contains(_selectedPage))
                {
                    Navigation.InsertPageBefore(page, _selectedPage);
                    await Navigation.PopAsync();
                }
                else
                {
                    await Navigation.PushAsync(page, false);

                    var list = Navigation.NavigationStack.ToList();

                    if (list != null)
                    {
                        foreach (var item in list)
                        {
                            if (item != page)
                                Navigation.RemovePage(item);
                        }
                    }                                     
                }

                _selectedPage = page;
            }
        }

        private void ItemClicked(object sender, MenuLeftItem menuItem)
        {
            if (!menuItem.IsEnabled)
                return;
            
            if (_selectedMenuItem != menuItem)
            {
                _selectedMenuItem.IsSelected = false;
                menuItem.IsSelected = true;
                _selectedMenuItem = menuItem;

                changeCurrentView(menuItem.IconSource);
            }
        }

        public override void DisposeResources()
        {

        }
    }
}

