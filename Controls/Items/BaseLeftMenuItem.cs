using System;
using Xamarin.Forms;

namespace voltaire.Controls.Items
{
    public class BaseLeftMenuItem : ContentView, ILeftMenuItem
    {
        public event EventHandler<MenuItem> ItemClicked;

        public void AddTapHandler(Layout layout)
        {
            var tapGestureRecognizer = new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    ItemClicked?.Invoke(this, (MenuItem)BindingContext);
                })
            };
            layout.GestureRecognizers.Add(tapGestureRecognizer);
        }
    }
}
