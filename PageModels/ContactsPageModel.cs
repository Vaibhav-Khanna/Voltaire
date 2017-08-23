using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using voltaire.Helpers.Collections;
using voltaire.Models;
using voltaire.PageModels.Base;

namespace voltaire.PageModels
{
    public class ContactsPageModel : BasePageModel
    {
        public string CustomersCount { get; set; }
        public ObservableCollection<Customer> customers { get; set; }

        public ObservableCollection<ObservableGroupCollection<string, CustomerModel>> CustomersItems { get; set; }

        public ContactsPageModel()
        {


        }

        public override void Init(object initData)
        {
            customers = new ObservableCollection<Customer>
            {
                new Customer {
                    FirstName= "Bill",
                    LastName="Anderson",
                    Grade="Pro",
                    Weight=5
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


            var models = customers.Select(i => new CustomerModel(i)).ToList();

            var groupedData =
                models.OrderBy(p => p.Customer.LastName)
                    .GroupBy(p => p.NameSort)
                    .Select(p => new ObservableGroupCollection<string, CustomerModel>(p))
                    .ToList();

            CustomersItems = new ObservableCollection<ObservableGroupCollection<string, CustomerModel>>(groupedData);




        }





    }
}
