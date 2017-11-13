using voltaire.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FreshMvvm;
using voltaire.PageModels;
using System.Linq;

namespace voltaire.Controls.Cells
{

    public partial class ContactCell
    {

        public ContactCell()
        {
            InitializeComponent();
        }
      

        protected override void OnTapped()
        {
            base.OnTapped();

            var model = (CustomerModel)BindingContext;

            model.Customer.CanEdit = false;

            model.navigation.PushPageModel<ContactDetailPageModel>(model.Customer);

            var parent = this.Parent as ListView;
            parent.SelectedItem = null;
        }


    }
}