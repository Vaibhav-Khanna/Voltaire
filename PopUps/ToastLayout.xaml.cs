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
            Device.StartTimer(new TimeSpan(0, 0, 2), HandleFunc);
        }

        bool HandleFunc()
        {
            Device.BeginInvokeOnMainThread( () => 
            {
                container.Padding = 0;
                container.HeightRequest = 20;
                anim.HeightRequest = 20;
                anim.WidthRequest = 20;
                anim.HorizontalOptions = LayoutOptions.Center;
                anim.TranslationX = -100;
                ToastLabel.Text = "";
            });
            
            return false;
        }
    }
}
