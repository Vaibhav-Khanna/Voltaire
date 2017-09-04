using voltaire.Models;
using Xamarin.Forms;
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
                new Span { Text = model.Salesman.FirstName, FontSize = 20, FontFamily="SanFranciscoDisplay-Regular"},
                new Span { Text = $" {model.Salesman.LastName}", FontSize = 20, FontFamily="SanFranciscoDisplay-Bold"} }
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




