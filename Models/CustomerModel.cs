using System.Text.RegularExpressions;
using FreshMvvm;

namespace voltaire.Models
{
    public class CustomerModel : BaseModel
    {
        public Customer Customer { get; set; }

        public IPageModelCoreMethods navigation { get; set; }

        public string Name => $"{Customer.FirstName} {Customer.LastName}";

        public string NameSort
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Customer.LastName) || Customer.LastName.Length == 0 || Regex.IsMatch(Customer.LastName, @"^\d")) return "#";
                return Customer.LastName[0].ToString().ToUpper();
            }
        }

        public CustomerModel(Customer customer)
        {
            Customer = customer;
        }
    }
}