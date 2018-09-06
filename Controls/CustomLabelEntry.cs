using System;
using voltaire.Renderers;
using voltaire.Resources;
using Xamarin.Forms;

namespace voltaire.Controls
{
    public class CustomLabelEntry : ContentView
    {

        Grid container;

        BorderlessEntry entry;
        Button cancelbutton;
        bool ShowText;
        Label labeltext;
        bool isdisabledstyle;

        public delegate void EventHandler();

        public event EventHandler FocusChanged;

		public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text", typeof(string), typeof(CustomLabelEntry), null);


        string placeholder;
        public string PlaceHolder { get { return placeholder; } set { placeholder = value; entry.Placeholder = value; } }


        Keyboard keyboard;
        public Keyboard Keyboard { get { return keyboard; } set { keyboard = value; entry.Keyboard = keyboard; } }

        string imagesource;
        public string ImageSource { get { return imagesource; } set { imagesource = value; labeltext.Text = value; OnPropertyChanged(nameof(ImageSource)); } }

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

        public CustomLabelEntry(string image, bool isText = false, bool isDisabledStyle = false)
        {
            ShowText = isText;
            isdisabledstyle = isDisabledStyle;
          
            imagesource = image;
            InflateLayout();
        }

        void InflateLayout()
        {
            
            container = new Grid()
            {              
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White,
                Padding = new Thickness(0,0,40,0),
                ColumnSpacing = 25
            };

            container.RowDefinitions.Add(new RowDefinition(){ Height = new GridLength(68, GridUnitType.Absolute)});
            container.ColumnDefinitions.Add(new ColumnDefinition(){ Width = new GridLength(26, GridUnitType.Auto)});
            container.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100, GridUnitType.Star) });
            container.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(Device.RuntimePlatform == Device.Android ? 36 : 26, GridUnitType.Absolute) });


            var img = new Image() { Source = imagesource , WidthRequest = 24,HeightRequest = 24, IsOpaque = true, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start };

            entry = new BorderlessEntry() { FontFamily = "SanFranciscoDisplay-Regular", FontSize = 20, HorizontalTextAlignment = TextAlignment.Start, TextColor = Color.Black, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.Center };

            entry.Focused += entryFocused;
            entry.Unfocused += Cancelbutton_Unfocused;

            cancelbutton = new Button { WidthRequest = 20, HeightRequest = 20, BackgroundColor = Color.Red, Text = "X",TextColor = Color.White, BorderColor = Color.Red, BorderRadius = 10, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.End };

            if (Device.RuntimePlatform == Device.Android)
            {
                cancelbutton.WidthRequest = 36;
                cancelbutton.HeightRequest = 36;
                cancelbutton.BorderRadius = 18;
                cancelbutton.FontSize = 12;
                cancelbutton.BorderWidth = 1;                   
            }

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
                container.Children.Add(labeltext,0,0);
            }
            else
            container.Children.Add(img,0,0);

            container.Children.Add(entry,1,0);
            container.Children.Add(cancelbutton,2,0);

            if(isdisabledstyle)
            {                
                labeltext.TextColor = (Color)App.Current.Resources["GreyishBrown"];
                cancelbutton.IsVisible = false;
            }

            Content = container;
        }


        void Cancelbutton_Clicked(object sender, EventArgs e)
        {
            Text = "";
            //entry.Focus();
        }

        void entryFocused (object sender, FocusEventArgs e)
        {
            if(e.IsFocused)
            FocusChanged?.Invoke();
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

