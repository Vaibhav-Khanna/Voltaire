using System;
using System.Collections.ObjectModel;
using voltaire.Controls;
using voltaire.Models;
using voltaire.PageModels.Base;
using Xamarin.Forms;
using voltaire.Resources;
using voltaire.PopUps;
using Rg.Plugins.Popup.Services;
using Acr.UserDialogs;
using System.Linq;

namespace voltaire.PageModels
{
    
    public class ContactAddPageModel : BasePageModel
    {

        AddTagsPopUpModel Popup_context = new AddTagsPopUpModel();

        AddCustomerPopUpModel Customer_Popup = new AddCustomerPopUpModel();


        public Command AddTags => new Command( async() =>
       {
            Popup_context.ItemSelectedChanged += Popup_Context_ItemSelectedChanged;
            await PopupNavigation.PushAsync(new AddTagsPopUp() { BindingContext = Popup_context }, true);
       });


        public Command AddCustomer => new Command(async() =>
       {
            Customer_Popup = new AddCustomerPopUpModel();
            Customer_Popup.ItemSelectedChanged += CustomerAdded;
            await PopupNavigation.PushAsync(new AddCustomerPopUp() { BindingContext = Customer_Popup }, true);
       });

        void CustomerAdded()
        {
            if (!string.IsNullOrEmpty(Customer_Popup.SelectedItem))
            {
                CompanyName = Customer_Popup.SelectedItem;
            }

            Customer_Popup.ItemSelectedChanged -= CustomerAdded;
        }

        void Popup_Context_ItemSelectedChanged()
        {
            if (!string.IsNullOrEmpty(Popup_context.SelectedItem))
            {
                Tags.Clear();
                Tags.Add(new TagControlModel(Tags) { TagText = Popup_context.SelectedItem, CanEdit = CanEdit });
            }

            Popup_context.ItemSelectedChanged -= Popup_Context_ItemSelectedChanged;
        }

        public Command SaveContact => new Command( async(obj) =>
       {

           if (string.IsNullOrWhiteSpace(Name))
           {
                await CoreMethods.DisplayAlert(AppResources.Alert, AppResources.FillInCustomerName, AppResources.Ok);
               return;
           }

            Dialog.ShowLoading(null);

            CanEdit = false;

            var customer = new Partner() { Name = Name, CompanyName = CompanyName, Phone = Phone, Email = Email, Website = Website, Comment = NoteText, PartnerWeight = Weight != null ? Convert.ToInt64(Weight) : 0 , ContactAddress = Address };
          
            if (Tags.Any() && ContactsPageModel.GradeValues.Any())
               customer.GradeId = ContactsPageModel.GradeValues[Tags.First().TagText].Value;

            await StoreManager.CustomerStore.InsertAsync(customer);

            Dialog.HideLoading();

            await CoreMethods.PopPageModel(customer);

            CanEdit = true; 

       });

        bool canedit = true;
        public bool CanEdit 
        {
            get { return canedit; }
            set
            {
                canedit = value;
                RaisePropertyChanged();
            }
        }


        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged();
            }
        }


        private int? weight = 1;
        public int? Weight
        {
            get { return weight; }
            set
            {
                weight = value;
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
                RaisePropertyChanged();
            }
        }


        private ObservableCollection<TagControlModel> tags = new ObservableCollection<TagControlModel>();
        public ObservableCollection<TagControlModel> Tags
        {
            get { return tags; }
            set
            {
                tags = value;

                RaisePropertyChanged();
            }
        }



    }
}
