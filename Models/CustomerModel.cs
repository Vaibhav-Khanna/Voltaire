using System.Linq;
using System.Text.RegularExpressions;
using FreshMvvm;
using voltaire.PageModels;
using Xamarin.Forms;

namespace voltaire.Models
{
    public class CustomerModel : BaseModel
    {


        public Partner Customer { get; set; }

        public IPageModelCoreMethods navigation { get; set; }

        public string Name => Customer.Name;

        public FormattedString DisplayText { get; set; }

       
        private string _grade;      
        public string Grade { get { return _grade; } set { _grade = value; RaisePropertyChanged(); } }

        public string NameSort
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Customer.Name) || Customer.Name.Length == 0 || Regex.IsMatch(Customer.Name.Trim(), @"^\d")) return "#";
                return Customer.Name.Trim()[0].ToString().ToUpper();
            }
        }

        public CustomerModel(Partner customer)
        {
            Customer = customer;

            var name_array = Customer.Name?.Split(' ');
           // var name = Customer.Name;

            DisplayText = new FormattedString
            {
                Spans = {
                    new Span { Text = name_array?.First() + " " , ForegroundColor = Color.Black, FontAttributes = FontAttributes.None, FontSize = 20, FontFamily="SanFranciscoDisplay-Regular"}
                }
            };

            if(name_array?.Count() > 1)
            {
                string st ="";
                var s = name_array.ToList();
                s.RemoveAt(0);
                foreach (var item in s)
                {
                    st += " " + item;
                }
                s = null;
                DisplayText.Spans.Add(new Span { Text = st.Trim(), ForegroundColor = Color.Black, FontAttributes = FontAttributes.Bold, FontSize = 20, FontFamily = "SanFranciscoDisplay-Bold" });
            }

            if (ContactsPageModel.GradeValues != null && ContactsPageModel.GradeValues.Any())
            {
                Grade = ContactsPageModel.GradeValues.Where((arg) => arg.Value == Customer?.GradeId)?.FirstOrDefault().Key;

                if(Grade!=null)
                {
                    Grade = "   " + Grade + "   "; 
                }
            }
        }
    }
}