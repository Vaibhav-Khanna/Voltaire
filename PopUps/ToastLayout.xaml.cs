using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace voltaire.PopUps
{
    public partial class ToastLayout : PopupPage
    {
        bool ShouldAnimate = false;

        public ToastLayout(string Text)
        {
           
            InitializeComponent();
            ToastLabel.Text = Text;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
         
            anim.Play();
            Device.StartTimer(new TimeSpan(0, 0, 3), HandleFunc);
        }

        bool HandleFunc()
        {
            Device.BeginInvokeOnMainThread( () => 
            {
                container.Padding = 0;
                container.LayoutTo(new Rectangle(0,0,container.Width,20),500,Easing.Linear);
                container.HeightRequest = 20;
                anim.HeightRequest = 20;
                anim.WidthRequest = 20;
                anim.HorizontalOptions = LayoutOptions.Center;
                anim.TranslationX = -100;
                anim.Play();
                ToastLabel.Text = "";
                if (!ShouldAnimate)
                {
                    ShouldAnimate = true;
                    Animate();
                }
            });
            
            return false;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ShouldAnimate = false;
        }

        async void Animate()
        {
            here:  await container?.FadeTo(0,1000,Easing.Linear);
            await container?.FadeTo(1, 1000, Easing.Linear);
           
            if(ShouldAnimate)
            goto here;
        }

    }
}
