﻿using Xamarin.Forms;

namespace voltaire.Behaviors
{
    public class MaxLengthValidatorBehavior : Behavior<Entry>
	{
		public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create("MaxLength", typeof(int), typeof(MaxLengthValidatorBehavior), 0);

		public int MaxLength
		{
			get { return (int)GetValue(MaxLengthProperty); }
			set { SetValue(MaxLengthProperty, value); }
		}

		protected override void OnAttachedTo(Entry bindable)
		{
			bindable.TextChanged += bindable_TextChanged;
		}

		private void bindable_TextChanged(object sender, TextChangedEventArgs e)
		{
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                return;

			if (e.NewTextValue.Length >= MaxLength)
				((Entry)sender).Text = e.NewTextValue.Substring(0, MaxLength);
		}

		protected override void OnDetachingFrom(Entry bindable)
		{
			bindable.TextChanged -= bindable_TextChanged;

		}
	}
}