﻿using Xamarin.Forms.Xaml;
namespace voltaire.Controls.Items
{
    public partial class LeftMenuItem
    {
        public LeftMenuItem()
        {
            InitializeComponent();
            AddTapHandler(ItemLayout);
        }
    }
}