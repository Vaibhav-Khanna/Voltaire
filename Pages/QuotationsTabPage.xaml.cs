using System;
using System.Collections.Generic;
using Xamarin.Forms;
using voltaire.Pages.Base;

namespace voltaire.Pages
{
    public partial class QuotationsTabPage : BaseViewPagerPage
    {
        public QuotationsTabPage()
        {
            InitializeComponent();

            #region UI_tweaks

           

            #endregion


        }

       

        protected override void BindingContextSet()
        {
            base.BindingContextSet();

            picker.SelectedIndex = 0;
        }

    }
}
