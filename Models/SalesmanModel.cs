using System.Text.RegularExpressions;
using FreshMvvm;

namespace voltaire.Models
{
    public class SalesmanModel : BaseModel
    {
        public Salesman Salesman { get; set; }

        public IPageModelCoreMethods navigation { get; set; }

        public string Name => $"{Salesman.FirstName} {Salesman.LastName}";

        public string NameSort
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Salesman.LastName) || Salesman.LastName.Length == 0 || Regex.IsMatch(Salesman.LastName, @"^\d")) return "#";
                return Salesman.LastName[0].ToString().ToUpper();
            }
        }

        public SalesmanModel(Salesman salesman)
        {
            Salesman = salesman;
        }
    }
}