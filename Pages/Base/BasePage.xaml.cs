using System.Collections.Generic;
using System.Linq;
using FreshMvvm;
using voltaire.Controls.Items;
using voltaire.Models;
using voltaire.PageModels;
using Xamarin.Forms;
namespace voltaire.Pages.Base
{
    public partial class BasePage : BaseDisposePage
    {
        private MenuItem _selectedMenuItem;
        private List<MenuItem> _pageIcons;
        private Page _selectedPage;

        public BasePage()
        {
            InitializeComponent();

        }
        public class MenuItem : BaseModel
        {
            public bool IsSelected { get; set; }
            public string Title { get; set; }
            public string IconSource { get; set; }
        }

        protected void SetMenu(StackLayout view, int selectedIndex)
        {
            if (_pageIcons != null && _pageIcons.Any()) return;

            _pageIcons = new List<MenuItem>
            {
                new MenuItem {Title = "", IsSelected = false, IconSource = "home"},
                new MenuItem {Title = "", IsSelected = false, IconSource = "contact"},
                new MenuItem {Title = "", IsSelected = false, IconSource = "map"},
                new MenuItem {Title = "", IsSelected = false, IconSource = "todo"},
                new MenuItem {Title = "", IsSelected = false, IconSource = "agenda"},
                new MenuItem {Title = "", IsSelected = false, IconSource = "report"},
                new MenuItem {Title = "", IsSelected = false, IconSource = "quotation"},
                new MenuItem {Title = "", IsSelected = false, IconSource = "contract"},
                new MenuItem {Title = "", IsSelected = false, IconSource = "goals"},
                new MenuItem {Title = "", IsSelected = false, IconSource = "podiums"},
            };
            _pageIcons[selectedIndex].IsSelected = true;
            _selectedMenuItem = _pageIcons[selectedIndex];

            foreach (var item in _pageIcons)
            {
                var cell = new LeftMenuItem();
                cell.BindingContext = item;
                //cell.ItemClicked += ItemClicked;
                view.Children.Add(cell);
            }
        }

        protected void changeCurrentView(string pageName)
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
                    page = FreshPageModelResolver.ResolvePageModel<MapPageModel>();
                    break;
                case "todo":
                    page = FreshPageModelResolver.ResolvePageModel<TodoPageModel>();
                    break;
                case "agenda":
                    page = FreshPageModelResolver.ResolvePageModel<AgendaPageModel>();
                    break;
                case "report":
                    page = FreshPageModelResolver.ResolvePageModel<ReportPageModel>();
                    break;
                case "quotations":
                    page = FreshPageModelResolver.ResolvePageModel<QuotationsPageModel>();
                    break;
                case "contract":
                    page = FreshPageModelResolver.ResolvePageModel<ContractPageModel>();
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
                if (_selectedPage != null)
                {
                    Navigation.InsertPageBefore(page, _selectedPage);
                    Navigation.PopAsync();
                }
                else
                {
                    Navigation.PushAsync(page, false);
                }
                _selectedPage = page;
            }
        }

        private void ItemClicked(object sender, MenuItem menuItem)
        {
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

