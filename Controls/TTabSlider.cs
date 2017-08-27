using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using voltaire.Controls.Items;
using Xamarin.Forms;
using System.Collections;
using System.Collections.Specialized;

namespace voltaire.Controls
{
    public class TTabSlider : ContentView
    {

        public delegate void EventHandler();

        public event EventHandler SelectedIndexChanged;

		public static readonly BindableProperty TabsProperty =
			BindableProperty.Create("Tabs", typeof(IEnumerable), typeof(TTabSlider), defaultValue: null); 

       
        public IEnumerable Tabs
		{
			get
            { 
                return (IEnumerable)GetValue(TabsProperty);
            }
			set 
            {
                SetValue(TabsProperty, value);
                OnPropertyChanged(nameof(Tabs));
            }
		}

		public static readonly BindableProperty SelectedIndexProperty =
			BindableProperty.Create("Selected_Index", typeof(int), typeof(TTabSlider), 0 );


		public int Selected_Index
        {
            get
            {
                return (int) GetValue(SelectedIndexProperty);
            }
            set
            {
                SetValue(SelectedIndexProperty,value);
				OnPropertyChanged(nameof(Selected_Index)); 
            }
        }

        StackLayout Container;
        ScrollView scrollview;
        BoxView slider;
        TapGestureRecognizer tap_tabgesture = new TapGestureRecognizer();   // tab tap gesture handler


        public TTabSlider()
        {
			InitView();
        }


        void Tabs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)  // handle change of tabviews
        {
            if (Tabs == null)
                return;
            
            Container.Children.Clear();
            foreach (var item in Tabs)
            {
                Container.Children.Add(GenerateTab(((TTab)item).Name));
            }
        }


        void InitView()                 // Initialize View
		{
            Container = new StackLayout() { Spacing = 40, Padding = new Thickness(50,10,50,10), Margin = 0, HorizontalOptions = LayoutOptions.CenterAndExpand, Orientation = StackOrientation.Horizontal };

            tap_tabgesture.Tapped += (sender, e) => 
            {
                var selected_tab = sender as Label;

                if(Container.Children.Contains(selected_tab))
                {
                    var index = Container.Children.IndexOf(selected_tab);
                    Selected_Index = index;
                }
            };

            slider = new BoxView { HeightRequest = 5, Color = (Color)Application.Current.Resources["Squash"], WidthRequest = 5, HorizontalOptions = LayoutOptions.Start, Margin = 0 };

            var main_Container = new StackLayout { Orientation = StackOrientation.Vertical, Spacing = 4, HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start, Children = { Container,slider }, Padding = 0, Margin = 0 };

            scrollview = new ScrollView { Orientation = ScrollOrientation.Horizontal, BackgroundColor = (Color)Application.Current.Resources["turquoiseBlue"],  Content = main_Container, HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = 0, Margin = 0 };

            Content = scrollview;
        }



        Label GenerateTab(string name)         //GenerateTabLabels
		{
            var label = new Label { FontFamily = "Raleway-SemiBold", Text = name.ToUpper(), TextColor = Color.FromRgba(255,255,255,178) };
            label.GestureRecognizers.Add(tap_tabgesture);
            return label;
        }


        void SelectTab()         //  Select  a tab view
        {
            if (Container.Children.Count == 0)
                return;

            foreach (Label item in Container.Children)
            {
                item.TextColor = Color.FromRgba(255, 255, 255, 178);
            }

            var new_selected = (Label) Container.Children[Selected_Index];
           
            if(new_selected!=null)
            {
                new_selected.TextColor = Color.White;
                scrollview.ScrollToAsync(new_selected,ScrollToPosition.Center,true);
                slider.WidthRequest = new_selected.Width;
                slider.TranslationX = Container.X + new_selected.X;
            }
        }


		protected override void OnPropertyChanged(string propertyName)
		{
			base.OnPropertyChanged(propertyName);

			switch (propertyName)
			{
				case "Tabs":
					Tabs_CollectionChanged(null, null);
                    break;

                case "Selected_Index":
                    {
						SelectTab();
						SelectedIndexChanged?.Invoke();
                        break;
                    }                 					
			}
		}

        public void ViewHasAppeared()
        {
            var child_view = Container.Children[Selected_Index];
            slider.TranslationX = Container.X + child_view.X;
            slider.WidthRequest = child_view.Width;
        }


    }
}

