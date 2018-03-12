using System;
using System.Collections.Generic;
using voltaire.PageModels;
using Xamarin.Forms;

namespace voltaire.Pages
{
    public partial class PermanentNotePage 
    {
        public PermanentNotePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            (BindingContext as PermanentNotePageModel).BackButton.Execute(null);
            return true;
        }
    }
}
