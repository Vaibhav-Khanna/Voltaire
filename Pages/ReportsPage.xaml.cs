using Xamarin.Forms;

namespace voltaire.Pages
{
    public partial class ReportsPage
    {

        private int _counter = 1;
        private double _width;
        private double _height;

        public ReportsPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetMenu(MenuLayout, 5);
            _width = Width;
            _height = Height;

            //if (_width < width || _height < height)
            //{
            //    RelativeLayout.Children.Add(ColorGrid,
            //    xConstraint: Constraint.RelativeToParent((parent) =>
            //    {
            //        return parent.Width / 2 + (parent.Width / 4);
            //    }),
            //    yConstraint: Constraint.RelativeToParent((parent) =>
            //    {
            //        return parent.Height / 6d;
            //    }));
            //}
            //else
            //{
                RelativeLayout.Children.Add(ColorGrid,
                xConstraint: Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width / 2 - 80d;
                }),
                yConstraint: Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height / 6d;
                }));
           // }
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
           
            //if (_width < width || _height < height)
            //{
            //    RelativeLayout.Children.Add(ColorGrid,
            //    xConstraint: Constraint.RelativeToParent((parent) =>
            //    {
            //        return parent.Width / 2 + (parent.Width / 4);
            //    }),
            //    yConstraint: Constraint.RelativeToParent((parent) =>
            //    {
            //        return parent.Height / 6d;
            //    }));
            //}
            //else
            //{
            //    RelativeLayout.Children.Add(ColorGrid,
            //    xConstraint: Constraint.RelativeToParent((parent) =>
            //    {
            //        return parent.Width / 2 - 80d;
            //    }),
            //    yConstraint: Constraint.RelativeToParent((parent) =>
            //    {
            //        return parent.Height / 6d;
            //    }));
            //}

        }
    }
}
