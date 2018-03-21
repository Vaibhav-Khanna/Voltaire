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
using Xamarin.Forms.GoogleMaps;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using voltaire.Models.DataObjects;

namespace voltaire.PageModels
{
    
    public class ContactAddPageModel : BasePageModel
    {

        AddTagsPopUpModel Popup_context = new AddTagsPopUpModel();

        AddCustomerPopUpModel Customer_Popup = new AddCustomerPopUpModel();

        Geocoder geocoder;

        Position position = new Position();

        Partner SearchedPartner;

        public ContactAddPageModel()
        {
            FetchAdditionalData();
        }

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
            if (Customer_Popup.SelectedItem != null)
            {
                SearchedPartner = Customer_Popup.SelectedItem;
                CompanyName = Customer_Popup.SelectedItem.Name;
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

        public Command SaveContact => new Command(async (obj) =>
      {
          if (string.IsNullOrWhiteSpace(Name))
          {
              await CoreMethods.DisplayAlert(AppResources.Alert, AppResources.FillInCustomerName, AppResources.Ok);
              return;
          }

          Dialog.ShowLoading(null);

          CanEdit = false;

          await SearchLocation();

          var customer = new Partner() { Name = Name, ParentName = CompanyName, Phone = Phone, Email = Email, Website = Website, Comment = NoteText, PartnerWeight = Weight != null ? Convert.ToInt64(Weight) : 0, Street = street1, Street2 = street2, City = city, Zip = zip, PartnerLatitude = position.Latitude, PartnerLongitude = position.Longitude };

          if (!string.IsNullOrEmpty(State) && States != null)
          {
              customer.StateId = States[StateIndex].ExternalId;
          }

          if (!string.IsNullOrEmpty(Country) && Countries != null)
          {
              customer.CountryId = Countries[CountryIndex].ExternalId;
          }


          if (SearchedPartner != null)
          {
              if (customer.ParentName?.Trim() == SearchedPartner.Name)
              {
                  customer.ParentId = SearchedPartner.ExternalId;
              }
          }

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


        public string Address
        {
            get { return $"{street1} {street2} {city} {State} {country} {zip}"; }
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

        string street1;
        public string Street1 { get { return street1; } set { street1 = value; RaisePropertyChanged(); } }

        string street2;
        public string Street2 { get { return street2; } set { street2 = value; RaisePropertyChanged(); } }

        string zip;
        public string Zip { get { return zip; } set { zip = value; RaisePropertyChanged(); } }

        string city;
        public string City { get { return city; } set { city = value; RaisePropertyChanged(); } }

        string state;
        public string State { get { return state; } set { state = value; RaisePropertyChanged(); } }

        string country;
        public string Country { get { return country; } set { country = value; RaisePropertyChanged(); } }

        int stateIndex;
        public int StateIndex { get { return stateIndex; } set { stateIndex = value; if (StateItems != null) State = StateItems[value]; RaisePropertyChanged(); } }

        int countryIndex;
        public int CountryIndex { get { return countryIndex; } set { countryIndex = value; if (CountryItems != null) Country = CountryItems[value]; RaisePropertyChanged(); } }

        ObservableCollection<string> _stateItems = new ObservableCollection<string>();
        public ObservableCollection<string> StateItems { get { return _stateItems; } set { _stateItems = value; RaisePropertyChanged(); } }

        ObservableCollection<string> _countryItems = new ObservableCollection<string>();
        public ObservableCollection<string> CountryItems { get { return _countryItems; } set { _countryItems = value; RaisePropertyChanged(); } }

        List<Country> Countries = new List<Country>();

        List<Models.DataObjects.State> States = new List<Models.DataObjects.State>();



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

		public override void Init(object initData)
		{
            base.Init(initData);

            geocoder = new Geocoder();
		}

        async Task SearchLocation()
        {
            if (String.IsNullOrWhiteSpace(Address))
                return;
            try
            {
                var possibleAddresses = await geocoder.GetPositionsForAddressAsync(Address);

                if (possibleAddresses != null && possibleAddresses.Any())
                {
                    position = possibleAddresses.FirstOrDefault();

                    foreach (var item in possibleAddresses)
                    {
                        Debug.WriteLine($"Lat:{item.Latitude}, Long:{item.Longitude}");
                    }
                }
            }
            catch(Exception)
            {
                return;
            }

        }

        async void FetchAdditionalData()
        {
            var t1 = await StoreManager.CountryStore.GetItemsAsync();

            var t2 = await StoreManager.StateStore.GetItemsAsync();

            Countries = t1?.ToList();

            States = t2?.ToList();

            CountryItems = new ObservableCollection<string>(Countries?.Select(x => x.Name));

            StateItems = new ObservableCollection<string>(States?.Select(x => x.Name));
           
        }

	}
}
