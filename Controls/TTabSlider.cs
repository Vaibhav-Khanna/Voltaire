using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using voltaire.Controls.Items;
using Xamarin.Forms;
using System.Collections;
using System.Collections.Specialized;
using voltaire.Renderers;
using System.Collections.Generic;

namespace voltaire.Controls
{
    public class TTabSlider : ContentView
    {

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
			BindableProperty.Create("SelectedIndex", typeof(int), typeof(TTabSlider), 0 );


		public int SelectedIndex
        {
            get
            {
                return (int) GetValue(SelectedIndexProperty);
            }
            set
            {
                SetValue(SelectedIndexProperty,value);
				OnPropertyChanged(nameof(SelectedIndex)); 
            }
        }

        StackLayout Container;
        CustomScrollView scrollview;
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
            Container = new StackLayout() { Spacing = 40, Padding = new Thickness(50,10,50,10), Margin = 0, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.StartAndExpand, Orientation = StackOrientation.Horizontal };

            tap_tabgesture.Tapped += (sender, e) => 
            {
                var selected_tab = sender as Label;

                if(Container.Children.Contains(selected_tab))
                {
                    var index = Container.Children.IndexOf(selected_tab);
                    SelectedIndex = index;
                }
            };

            slider = new BoxView { HeightRequest = 5, Color = (Color)Application.Current.Resources["Squash"], WidthRequest = 5, HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.End, Margin = 0 };

            var main_Container = new StackLayout {
                Orientation = StackOrientation.Vertical,
                Spacing = 4,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand, Children = { Container,slider }, Padding = 0, Margin = 0 };

            scrollview = new CustomScrollView { Orientation = ScrollOrientation.Horizontal, BackgroundColor = (Color)Application.Current.Resources["turquoiseBlue"],  Content = main_Container, HorizontalOptions = LayoutOptions.FillAndExpand,
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

            var new_selected = (Label) Container.Children[SelectedIndex];
           
            if(new_selected!=null)
            {
                new_selected.TextColor = Color.White;
                if (Device.RuntimePlatform == Device.iOS)
                {
                    scrollview.ScrollToAsync(new_selected, ScrollToPosition.Center, true);
                }
                slider.WidthRequest = new_selected.Width;
                slider.TranslateTo(Container.X + new_selected.X, 0);
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

                case "SelectedIndex":
                    {
						SelectTab();
                        break;
                    }                 					
			}
		}

        public void ViewHasAppeared()
        {
            if (Container.Children.Count == 0)
                return;
            
            var child_view = Container.Children[SelectedIndex];
            slider.TranslationX = Container.X + child_view.X;
            slider.WidthRequest = child_view.Width;
        }

    }
}

