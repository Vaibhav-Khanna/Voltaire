using System;
using voltaire.Renderers;
using voltaire.Resources;
using Xamarin.Forms;

namespace voltaire.Controls
{
    public class CustomLabelEntry : ContentView
    {

        StackLayout container;
        string imagesource;
        BorderlessEntry entry;
        Button cancelbutton;
        bool ShowText;
        Label labeltext;

		public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text", typeof(string), typeof(CustomLabelEntry), null);

        Keyboard keyboard;

        public Keyboard Keyboard { get { return keyboard; } set { keyboard = value; entry.Keyboard = keyboard; } }

        public string Text
		{
			get
			{
                return (string)GetValue(TextProperty);
			}
			set
			{
				SetValue(TextProperty, value);
                entry.Text = Text;
				OnPropertyChanged(nameof(Text));
			}
		}

        public CustomLabelEntry(string image, bool isText = false)
        {
            ShowText = isText;
            imagesource = image;
            InflateLayout();
        }

        void InflateLayout()
        {
            
            container = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White,
                Padding = new Thickness(0,0,40,0),
                Spacing = 25
            };




            var img = new Image() { Source = imagesource , WidthRequest = 24,HeightRequest = 24, IsOpaque = true, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start };

            entry = new BorderlessEntry() { FontFamily = "SanFranciscoDisplay-Regular", FontSize = 20, HorizontalTextAlignment = TextAlignment.Start, TextColor = Color.Black, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.Center };

            entry.Unfocused += Cancelbutton_Unfocused;

            cancelbutton = new Button { WidthRequest = 20, HeightRequest = 20, BackgroundColor = Color.Red, Text = "X",TextColor = Color.White, BorderColor = Color.Red, BorderRadius = 10, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.End };

            cancelbutton.Clicked += Cancelbutton_Clicked;

			labeltext = new Label
			{
				TextColor = (Color)App.Current.Resources["turquoiseBlue"],
				FontFamily = "SanFranciscoDisplay-Regular",
				FontSize = 20,
				Text = imagesource,
				BackgroundColor = Color.White,
				HorizontalOptions = LayoutOptions.Start,
				HorizontalTextAlignment = TextAlignment.Start,
				VerticalOptions = LayoutOptions.Center,
				VerticalTextAlignment = TextAlignment.Center,
			};

			if (ShowText)
			{		
                container.Children.Add(labeltext);
            }
            else
            container.Children.Add(img);

            container.Children.Add(entry);
            container.Children.Add(cancelbutton);

            Content = container;
        }


        void Cancelbutton_Clicked(object sender, EventArgs e)
        {
            Text = "";
            entry.Focus();
        }

        void Cancelbutton_Unfocused(object sender, FocusEventArgs e)
        {
            Text = entry.Text;
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
			
            switch (propertyName)
			{
                case "IsEnabled":
					{
                        cancelbutton.IsVisible = this.IsEnabled;
                        if( IsEnabled )
                        {
                            labeltext.TextColor = (Color)App.Current.Resources["turquoiseBlue"];
                        }
                        else if(!IsEnabled)
                        {
                            labeltext.TextColor = (Color)App.Current.Resources["GreyPlaceholder"];
                        }
						break;
					}
                case "Text":
                    {
                        entry.Text = Text;
                        break;
                    }

			}


        }


    }
}

