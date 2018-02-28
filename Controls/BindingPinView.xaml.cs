using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace voltaire.Controls
{
    public partial class BindingPinView 
    {
		private string _display;
        private string _color;

        public BindingPinView(string display,Color color)
		{
			InitializeComponent();
			_display = display;
            tintimage.TintColor = color;
			BindingContext = this;
		}


		public string Display
		{
			get { return _display; }
		}
    }
}
