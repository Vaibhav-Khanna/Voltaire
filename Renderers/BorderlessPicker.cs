using System;
using Xamarin.Forms;

namespace voltaire.Renderers
{
    public class BorderlessPicker : Picker
    {
        public BorderlessPicker()
        {

        }

        public static readonly BindableProperty TextAlignMentProperty = BindableProperty.Create(
            nameof(TextAlignMent),
            typeof(TextAlignment),
            typeof(Picker),
            TextAlignment.End);


       
        public TextAlignment TextAlignMent
        {
            get { return (TextAlignment)GetValue(TextAlignMentProperty); }
            set { SetValue(TextAlignMentProperty, value); }
        }


    }
}
