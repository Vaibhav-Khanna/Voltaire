using System;
using FreshMvvm;
using System.Collections.ObjectModel;
using voltaire.Models;
using voltaire.PageModels.Base;
using Xamarin.Forms;
using System.Threading.Tasks;
using voltaire.Controls.Items;
using voltaire.TemplateSelectors;
using voltaire.Resources;

namespace voltaire.PageModels
{
    public class ContactDetailPageModel : BasePageModel
    {
        Customer _customer;

        public Command tap_Toolbar  => new Command(() => 
        {
            
        });

        public Command tap_Back  => new Command(async() =>
	   {
            await CoreMethods.PopPageModel();
            ReleaseResources();
	   });

        private ObservableCollection<TTab> tab;

        public ObservableCollection<TTab> Tab
        {
            get { return tab; } 
            set 
            {
                tab = value;
                RaisePropertyChanged();
            }
        }

        private int? weight;

		public int? Weight
		{
            get { return weight; }
			set
			{
                weight = value;
                customer.Weight = weight;
				RaisePropertyChanged();
			}
		}

        private DateTime? lastvisit;

        public DateTime? LastVisit
		{
			get { return lastvisit; }
			set
			{
				lastvisit = value;
                customer.LastVisit = lastvisit;
				RaisePropertyChanged();
			}
		}


        private string email;

        public string Email
		{
            get { return email; }
			set
			{
                email = value;
                customer.Email = email;
				RaisePropertyChanged();
			}
		}

		private string website;

		public string Website
		{
            get { return website; }
			set
			{
                website = value;
                customer.Website = website;
				RaisePropertyChanged();
			}
		}

		private string address;

		public string Address
		{
            get { return address; }
			set
			{
                address = value;
                customer.Address = address;
				RaisePropertyChanged();
			}
		}

		private string phone;

		public string Phone
		{
            get { return phone; }
			set
			{
                phone = value;
                customer.Phone = phone;
				RaisePropertyChanged();
			}
		}

        private int selectedindex = 0;

        public int SelectedIndex 
        {
            get { return selectedindex; }
            set
            {
                selectedindex = value;
                RaisePropertyChanged(nameof(SelectedIndex));
            }
        }

        ViewPagerTemplateSelector item_template_selector;

        public ViewPagerTemplateSelector ItemTemplates
        {
            get { return item_template_selector; }
            set
            {
                item_template_selector = value;
                RaisePropertyChanged();
            }
        }

        public Customer customer { get 
            {
                return _customer;
            } set
            {
                _customer = value;
              
                title = $"{customer.FirstName} {customer.LastName}";
              
                weight = customer.Weight;
                email = customer.Email;
                address = customer.Address;
                phone = customer.Phone;
                website = customer.Website;
                lastvisit = customer.LastVisit;

                RaisePropertyChanged();
                RaisePropertyChanged(nameof(Title));
                RaisePropertyChanged(nameof(Weight));
                RaisePropertyChanged(nameof(Email));
                RaisePropertyChanged(nameof(Website));
                RaisePropertyChanged(nameof(Address));
                RaisePropertyChanged(nameof(LastVisit));
            } 

        }

        public ContactDetailPageModel()
        {
            
        }

        private string title;

        public string Title 
        {
            get { return title; }
            set 
            {
                value =  title;

                RaisePropertyChanged();
            }
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            if(initData!=null)
            customer = (Customer)initData;

            ObservableCollection<TTab> pages = new ObservableCollection<TTab>();

            pages.Add(new TTab(this) { Name = AppResources.ContactDetails, View = typeof(Pages.ContactDetailTabPage) });
            pages.Add(new TTab(this) { Name = AppResources.Reminder, View = typeof(Pages.MapTabPage)  });
            pages.Add(new TTab(this) { Name = AppResources.Map, View = typeof(Pages.MapTabPage) });
            pages.Add(new TTab(this) { Name = AppResources.Quotations, View = typeof(Pages.MapTabPage) });
            pages.Add(new TTab(this) { Name = AppResources.Contracts , View = typeof(Pages.MapTabPage)  });           

            var selector = new ViewPagerTemplateSelector();

            foreach (var item in pages)
            {
                selector.PageTemplates.Add(new DataTemplate(item.View));
            }

            Tab = pages;
            ItemTemplates = selector;

        }

       

    }
}
