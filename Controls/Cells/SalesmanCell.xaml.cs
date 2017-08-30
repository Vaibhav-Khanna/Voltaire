using voltaire.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FreshMvvm;
using voltaire.PageModels;

namespace voltaire.Controls.Cells
{
    public partial class SalesmanCell
    {
        public SalesmanCell()
        {
            InitializeComponent();
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            var model = (SalesmanModel)BindingContext;


            if (model != null)
            {
                FullNameLabel.FormattedText = new FormattedString
                {
                    Spans = {
                new Span { Text = model.Salesman.FirstName, FontAttributes = FontAttributes.None, FontSize = 20, FontFamily="SanFranciscoDisplay-Regular"},
                new Span { Text = $" {model.Salesman.LastName}", FontAttributes = FontAttributes.Bold, FontSize = 20, FontFamily="SanFranciscoDisplay-Regular"} }
                };
            }
        }

        protected override void OnTapped()
        {
            base.OnTapped();

            var model = (SalesmanModel)BindingContext;

            //model.Customer.CanEdit = false;

            model.navigation.PushPageModel<GoalsDetailPageModel>(model.Salesman);

            var parent = this.Parent as ListView;
            parent.SelectedItem = null;
        }

    }
}




