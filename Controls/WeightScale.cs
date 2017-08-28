using System;
using voltaire.Resources;
using Xamarin.Forms;

namespace voltaire.Controls
{
    public class WeightScale : ContentView
    {

		int maxweight;

		StackLayout container;

		public delegate void EventHandler();

		public event EventHandler SelectedScaleChanged;

		public static readonly BindableProperty SelectedScaleProperty =
            BindableProperty.Create("SelectedScale", typeof(int?), typeof(WeightScale), -1);


		public int? SelectedScale
		{
			get
			{
				return (int?)GetValue(SelectedScaleProperty);
			}
			set
			{
                SetValue(SelectedScaleProperty, value);
                OnPropertyChanged(nameof(SelectedScale));	
			}
		}


        public WeightScale()
        {
            LayoutInflate();
        }


        public int MaxWeight
        {
            get { return maxweight; }
            set
            {
                maxweight = value;
                ChildLayout();
            }
        }


        void ChildLayout()
        {
            container.Children.Clear();
		
            var m = MaxWeight;

            for (int i = 0; i < m; i++)
            {
                container.Children.Add(InflateButton((i+1).ToString()));
            }
        }


        void LayoutInflate()
        {
            container = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 15,
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center
            };


            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.FillAndExpand;

            var lb = new Label
			{
                Text = AppResources.Weight,
				FontFamily = "SanFranciscoDisplay-Regular",
				FontSize = 20,
                TextColor = (Color)App.Current.Resources["GreyishBrown"],
				Margin = new Thickness(0, 0, 80, 0),
				BackgroundColor = Color.White,
				HorizontalOptions = LayoutOptions.Start,
				HorizontalTextAlignment = TextAlignment.Start,
				VerticalOptions = LayoutOptions.FillAndExpand,
				VerticalTextAlignment = TextAlignment.Center,
			};

            Content = new StackLayout{ Orientation = StackOrientation.Horizontal, Spacing= 0, Margin = 0,Padding = 0,
                Children = { lb, container },BackgroundColor = Color.White
            };

        }

        void Button_Clicked(object sender, EventArgs e)
        {
            var bt = sender as Button;

            if (bt == null)
                return;

            foreach (var item in container.Children)
            {
                var bt_item = item as Button;
                bt_item.TextColor = (Color)App.Current.Resources["GreyBack3"];
                bt_item.BorderColor = (Color)App.Current.Resources["GreyBack3"];
                bt_item.BackgroundColor = Color.Transparent;
            }

            bt.TextColor = Color.White;
            bt.BorderColor = (Color)App.Current.Resources["turquoiseBlue"];
            bt.BackgroundColor = (Color)App.Current.Resources["turquoiseBlue"];


            SelectedScaleChanged?.Invoke();
            SetValue(SelectedScaleProperty,Convert.ToInt32(bt.Text));

        }


        Button InflateButton(string number)
        {
            var button = new Button
            {
                WidthRequest = 36,
                Text = number,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 36,
                BackgroundColor = Color.Transparent,
                BorderRadius = 18,
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Start,
                BorderColor = (Color)App.Current.Resources["GreyBack3"],
                TextColor = (Color)App.Current.Resources["GreyBack3"],
                FontSize = 16
            };
            button.Clicked -= Button_Clicked;
            button.Clicked += Button_Clicked;
            return button;
        }


		protected override void OnPropertyChanged(string propertyName)
		{
			base.OnPropertyChanged(propertyName);

			switch (propertyName)
			{
				case "SelectedScale":
                    {
                        if (SelectedScale == null || SelectedScale == -1)
                            break;
                        
                        var bt = container.Children[ (int)SelectedScale - 1];
                        Button_Clicked(bt,null);
                        break;
                    }
					
			}
		}


    }
}

