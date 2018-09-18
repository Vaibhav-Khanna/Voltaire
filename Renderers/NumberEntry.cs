using System;
using voltaire.Behaviors;
using Xamarin.Forms;

namespace voltaire.Renderers
{
    public class NumberEntry : Entry
    {
        public NumberEntry()
        {
			Behaviors.Add(new NumberValidationBehavior());
			Behaviors.Add(new MaxLengthValidatorBehavior() { MaxLength = 2 });

            Unfocused += NumberEntry_Unfocused;
        }

        public NumberEntry(int maxLength)
        {
            Behaviors.Add(new NumberValidationBehavior());
            Behaviors.Add(new MaxLengthValidatorBehavior() { MaxLength = maxLength });

            Unfocused += NumberEntry_Unfocused;
        }

        void NumberEntry_Unfocused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Text))
                Text = "1";
        }
    }
}
