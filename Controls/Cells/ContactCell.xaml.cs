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

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
          
            var model = (CustomerModel)BindingContext;

            if (model == null)
                return;

            var name_array = model.Customer.Name?.Split(' ');

            if (model != null)
            {
                FullNameLabel.FormattedText = new FormattedString
                {
                    Spans = {
                        new Span { Text = name_array?.First() + " " , FontAttributes = FontAttributes.None, FontSize = 20, FontFamily="SanFranciscoDisplay-Regular"},
                        new Span { Text = name_array?.Count() > 1 ? name_array?.LastOrDefault() : string.Empty , FontSize = 20, FontFamily="SanFranciscoDisplay-Bold"} }
                };
            }
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