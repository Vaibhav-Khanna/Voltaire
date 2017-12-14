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

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            //if (BindingContext != null && ContactsPageModel.GradeValues != null && ContactsPageModel.GradeValues.Any() )
            //{
            //    var model = (CustomerModel)BindingContext;
            //    model.Grade = "   " + ContactsPageModel.GradeValues.Where( (arg) => arg.Value == model.Customer?.GradeId )?.FirstOrDefault().Key + "   ";
            //}
        }

    }
}