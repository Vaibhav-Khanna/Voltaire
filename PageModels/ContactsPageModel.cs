using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using FreshMvvm;
using voltaire.Helpers.Collections;
using voltaire.Models;
using voltaire.PageModels.Base;
using Xamarin.Forms;

namespace voltaire.PageModels
{
    public class ContactsPageModel : BasePageModel
    {
        public string CustomersCount { get; set; }
        public ObservableCollection<Customer> customers { get; set; }

        public ObservableCollection<ObservableGroupCollection<string, CustomerModel>> CustomersItems { get; set; }

        public ICommand FiltersLayoutCommand => new Command(FiltersLayoutAppearing);

        private int? _listColumnSpan;

        public int? listColumnSpan
        {

            get { return _listColumnSpan; }

            set { _listColumnSpan = value; RaisePropertyChanged("listColumnSpan"); }
        }

        private bool _filterLayoutVisibility;

        public bool filterLayoutVisibility
        {

            get { return _filterLayoutVisibility; }

            set { _filterLayoutVisibility = value; RaisePropertyChanged("filterLayoutVisibility"); }
        }

        private string _filterImage;

        public string filterImage
        {

            get { return _filterImage; }

            set { _filterImage = value; RaisePropertyChanged("filterImage"); }
        }

        public ContactsPageModel()
        {

        }

        private void FiltersLayoutAppearing()
        {
            if (listColumnSpan == 2)
            {
                listColumnSpan = 1;
                filterLayoutVisibility = true;
                filterImage = "close";
            }
            else if (listColumnSpan == 1)
            {
                listColumnSpan = 2;
                filterLayoutVisibility = false;
                filterImage = "filters";
            }

        }

        public ObservableCollection<PartnerGrade> partnerGrades { get; set; }

        //INIT data form page  freshmvvm
        public override void Init(object initData)
        {


            customers = new ObservableCollection<Customer>
            {
                new Customer {
                    FirstName= "Bill",
                    LastName="Anderson",
                    Grade="Pro",
                    Company = "Matsiya IT",
                    Weight= 4,
                    Email = "vaibhav@gmail.com",
                    Address = "Jammu and Kashmir",
                    Phone = "8872892265",
                    Website = "www.matisya.com",
                    LastVisit=new DateTime(2017, 6, 3),
                    CustomerAddresses = new List<CustomerAddressLocation>(){ new CustomerAddressLocation()
                        {
                            Address = "8 c Beauty Avenue, Phase 1, Amritsar, Punjab, 143001",
                            Title = "Matsiya",
                            Latitude = 31.655,
                            Longitude = 74.879,
                        }, new CustomerAddressLocation()
                        {
                            Address = "Metro Cash and carry",
                            Title = "Metro",
                            Latitude = 31.669,
                            Longitude = 74.882
                        } },
                   
                },
                new Customer {
                    FirstName= "Milton",
                    LastName="Aaron",
                    Grade="Client Amateur"
                },
                new Customer {
                    FirstName= "Reid",
                    LastName="Alex"
                },

                new Customer {
                    FirstName= "Fred",
                    LastName="Hojberg",
                    Grade="Client Amateur"
                },

                new Customer {
                    FirstName= "Bruce",
                    LastName="Ballard"
                },
                new Customer {
                    FirstName= "Alex",
                    LastName="Bartley",
                    Grade="Client Amateur"
                },
                new Customer {
                    FirstName= "Michael",
                    LastName="Jordan"
                },
                new Customer {
                    FirstName= "Magic",
                    LastName="Johnson"
                },
                new Customer {
                    FirstName= "Bill",
                    LastName="Russell",
                    Company="Antares",
                    LastVisit=new DateTime(2017, 1, 18)
                },
                new Customer {
                    FirstName= "James",
                    LastName="Harden",
                    Grade="Client Amateur"
                },
                new Customer {
                    FirstName= "Russell",
                    LastName="Westbrook",
                    Company="Centre Hippique de Lescar",
                    Grade="Pro",
                    LastVisit=new DateTime(2017, 4, 22),
                    Weight=2
                },
                new Customer {
                    FirstName= "Kevin",
                    LastName="Durant",
                    Grade="Client Amateur",
                    Weight=1
                },
                new Customer {
                    FirstName= "Shaquil",
                    LastName="O neill"
                },
                new Customer {
                    FirstName= "Lebron",
                    LastName="James",
                    Company="PMU",
                    Grade="Pro"
                },
                new Customer {
                    FirstName= "Derrick",
                    LastName="Rose"
                },
                new Customer {
                    FirstName= "Mike",
                    LastName="Dantoni"
                },
                new Customer {
                    FirstName= "Chris",
                    LastName="Paul",
                    Grade="Pro"
                },
                new Customer {
                    FirstName= "Bill",
                    LastName="Murray"
                },
                new Customer {
                    FirstName= "Jason",
                    LastName="Kid"
                },
                new Customer {
                    FirstName= "John",
                    LastName="Stockton"
                }

            };


            if (customers.Count > 1) CustomersCount = customers.Count + " Contacts";
            else CustomersCount = customers.Count + " Contact";


            var models = customers.Select(i => new CustomerModel(i) { navigation = CoreMethods }).ToList();

            var groupedData =
                models.OrderBy(p => p.Customer.LastName)
                    .GroupBy(p => p.NameSort)
                    .Select(p => new ObservableGroupCollection<string, CustomerModel>(p))
                    .ToList();

            CustomersItems = new ObservableCollection<ObservableGroupCollection<string, CustomerModel>>(groupedData);

            //columnSpan for listview
            _listColumnSpan = 2;
            //filters view
            _filterLayoutVisibility = false;
            //filter frame image 
            _filterImage = "filters";

            //PartnerGrades 
            partnerGrades = new ObservableCollection<PartnerGrade>
            {
                new PartnerGrade {Grade="CCE"},
                new PartnerGrade {Grade="CSO"},
                new PartnerGrade {Grade="DRE"},
                new PartnerGrade {Grade="Endurance"},
                new PartnerGrade {Grade="PRO"},
                new PartnerGrade {Grade="Particulier"},
                new PartnerGrade {Grade="Ecuries"}


            };

        }


    }
}
