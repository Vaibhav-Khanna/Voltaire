using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace voltaire.Controls
{
    public partial class BindingPinView 
    {
		private string _display;

		public BindingPinView(string display)
		{
			InitializeComponent();
			_display = display;
			BindingContext = this;
		}

		public string Display
		{
			get { return _display; }
		}
    }
}
