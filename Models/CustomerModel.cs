using System.Text.RegularExpressions;
using FreshMvvm;

namespace voltaire.Models
{
    public class CustomerModel : BaseModel
    {
        public Partner Customer { get; set; }

        public IPageModelCoreMethods navigation { get; set; }

        public string Name => Customer.Name;

        public string NameSort
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Customer.Name) || Customer.Name.Length == 0 || Regex.IsMatch(Customer.Name, @"^\d")) return "#";
                return Customer.Name[0].ToString().ToUpper();
            }
        }

        public CustomerModel(Partner customer)
        {
            Customer = customer;
        }
    }
}