using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace voltaire.PopUps
{
    public partial class ToastLayout : PopupPage
    {
        public ToastLayout(string Text)
        {
           
            InitializeComponent();
            ToastLabel.Text = Text;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            anim.Play();
        }

    }
}
