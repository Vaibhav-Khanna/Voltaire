using System;
using System.Collections.Generic;
using voltaire.Models;
using voltaire.PageModels;
using Xamarin.Forms;

namespace voltaire.Controls.Cells
{
    public partial class ReportCell
    {
        public ReportCell()
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

            model.navigation.PushPageModel<ReportsDetailPageModel>(model.Salesman);

            var parent = this.Parent as ListView;
            parent.SelectedItem = null;
        }

    }
}
