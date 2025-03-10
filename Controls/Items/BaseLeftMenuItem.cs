﻿using System;
using voltaire.Models;
using Xamarin.Forms;

namespace voltaire.Controls.Items
{
    public class BaseLeftMenuItem : ContentView, ILeftMenuItem
    {
        public event EventHandler<MenuLeftItem> ItemClicked;

        public void AddTapHandler(Layout layout)
        {
            var tapGestureRecognizer = new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    ItemClicked?.Invoke(this, (MenuLeftItem)BindingContext);
                })
            };
            layout.GestureRecognizers.Add(tapGestureRecognizer);
        }
    }
}
