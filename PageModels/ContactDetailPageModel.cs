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
using voltaire.Controls;
using voltaire.PopUps;
using Rg.Plugins.Popup.Services;
using System.Linq;
using System.Collections.Generic;

namespace voltaire.PageModels
{


    public class ContactDetailPageModel : BasePageModel
    {
       
        Customer _customer;

        public Command tap_Toolbar  => new Command( async () => 
        {
            if (!customer.CanEdit)
            {
                var customer_copy = customer;

                customer_copy.CanEdit = true;

                await CoreMethods.PushPageModel<ContactDetailPageModel>(customer_copy, true, true);
            }
            else
            {
                customer.Address = address;
                customer.Weight = weight;
                customer.FirstName = firstname;
                customer.LastName = lastname;
                customer.Phone = phone;
                customer.Website = website;
                customer.LastVisit = lastvisit;
				customer.PermanentNote = notetext;
                customer.Email = email;
                customer.CanEdit = false;
                customer.CompanyName = companyname;
                await CoreMethods.PopPageModel(customer, true, true); 
            }
        });


        public Command tap_Back  => new Command(async() =>
	   {
            
           if (customer.CanEdit)
           {
               customer.CanEdit = false;
               await CoreMethods.PopPageModel(null, true, true);
               ReleaseResources();
           }
           else
           {
               customer.CanEdit = false;
               await CoreMethods.PopPageModel(null, false, true);
               ReleaseResources();
           }

	   });


        public Command CheckIn => new Command( () =>
       {
            LastVisit = DateTime.Now;
       });


        AddTagsPopUpModel Popup_context = new AddTagsPopUpModel();

        public Command AddTags => new Command( async(obj) =>
       {
            Popup_context.ItemSelectedChanged += Popup_Context_ItemSelectedChanged;
            await PopupNavigation.PushAsync(new AddTagsPopUp() { BindingContext = Popup_context }, true);
       });


        void Popup_Context_ItemSelectedChanged()
        {
            if(!string.IsNullOrEmpty(Popup_context.SelectedItem))
            {
                Tags.Add(new TagControlModel(Tags,customer.Tags){ TagText = Popup_context.SelectedItem, CanEdit = CanEdit });
                customer.Tags.Add(Popup_context.SelectedItem);
            }
           
            Popup_context.ItemSelectedChanged -= Popup_Context_ItemSelectedChanged;
        }


        public Command InternalNotes => new Command(async () =>
        {
            await CoreMethods.PushPageModel<QuotationInternalNotesPageModel>(customer, false, true);
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

        private ObservableCollection<TagControlModel> tags;
        public ObservableCollection<TagControlModel> Tags
        {
            get { return tags; }
            set
            {
                tags = value;

                RaisePropertyChanged();
            }
        }

        private bool canedit;

        public bool CanEdit
		{
            get { return canedit; }
			set
			{
                canedit = value;	
				RaisePropertyChanged();
			}
		}

		private string backbutton;

		public string BackButton
		{
			get { return backbutton; }
			set
			{
				backbutton = value;
				RaisePropertyChanged();
			}
		}


        private string toolbarbutton;

        public string ToolbarButton
		{
			get { return toolbarbutton; }
			set
			{
				toolbarbutton = value;
				RaisePropertyChanged();
			}
		}

        private string firstname;

        public string FirstName
		{
			get { return firstname; }
			set
			{
				firstname = value;
				RaisePropertyChanged();
			}
		}

		private string lastname;

		public string LastName
		{
            get { return lastname; }
			set
			{
                lastname = value;
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
              
				RaisePropertyChanged();
			}
		}

        string companyname;
	
        public string CompanyName
		{
			get
			{
				return companyname;
			}

			set
			{	
				companyname = value;
                RaisePropertyChanged();
			}
		}

        string notetext;
        public string NoteText 
        {
            get { return notetext; }
            set
            {
                notetext = value;

                customer.PermanentNote = notetext;

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

                firstname = customer.FirstName;
                lastname = customer.LastName;
                weight = customer.Weight;
                email = customer.Email;
                address = customer.Address;
                phone = customer.Phone;
                website = customer.Website;
                lastvisit = customer.LastVisit;
                canedit = customer.CanEdit;
                NoteText = customer.PermanentNote;
                title = canedit ? AppResources.Update : $"{customer.FirstName} {customer.LastName}";
                toolbarbutton = canedit ? AppResources.Save : AppResources.Modify;
                backbutton = canedit ? AppResources.Cancel : AppResources.Back;
                companyname = customer.CompanyName;

                if (Tags == null)
                    Tags = new ObservableCollection<TagControlModel>();
                else
                    Tags.Clear();

                foreach (var item in customer.Tags)
                {
                    Tags.Add(new TagControlModel(Tags,customer.Tags){ TagText = item , CanEdit = CanEdit });
                }
                    
                //Tags = new ObservableCollection<TagControlModel>(Tags.ToList());

                RaisePropertyChanged(); 
                RaisePropertyChanged(nameof(Title));
                RaisePropertyChanged(nameof(FirstName));
                RaisePropertyChanged(nameof(LastName));
                RaisePropertyChanged(nameof(Weight));
                RaisePropertyChanged(nameof(Phone));
                RaisePropertyChanged(nameof(Email));
                RaisePropertyChanged(nameof(Website));
                RaisePropertyChanged(nameof(CompanyName));
                RaisePropertyChanged(nameof(Address));
                RaisePropertyChanged(nameof(LastVisit));
                RaisePropertyChanged(nameof(CanEdit));
                RaisePropertyChanged(nameof(ToolbarButton));
                RaisePropertyChanged(nameof(BackButton));
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

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);

            if (returnedData == null)
                return;

            customer = (Customer)returnedData;
     
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            if(initData!=null)
            customer = (Customer)initData;

            ObservableCollection<TTab> pages = new ObservableCollection<TTab>();

            pages.Add(new TTab(this) { Name = AppResources.ContactDetails, View = typeof(Pages.ContactDetailTabPage) });

   
            if (!CanEdit)
            {
                //pages.Add(new TTab(this) { Name = AppResources.Reminder, View = typeof(ContentView) });
                pages.Add(new TTab(this) { Name = AppResources.Map, View = typeof(Pages.MapTabPage) });
                pages.Add(new TTab(this) { Name = AppResources.Orders, View = typeof(Pages.OrderListTabPage) });
                pages.Add(new TTab(this) { Name = AppResources.Quotations, View = typeof(Pages.QuotationsTabPage) });
                pages.Add(new TTab(this) { Name = AppResources.Contracts, View = typeof(Pages.ContractListTabPage) });
            }

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
