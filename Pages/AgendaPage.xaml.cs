using System;
using System.Collections.Generic;
using Plugin.Geolocator;
using voltaire.Controls;
using voltaire.Models;
using voltaire.PageModels;
using voltaire.Resources;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace voltaire.Pages
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgendaPage 
    {

        public AgendaPageModel ViewModel { get; set; }
        private Pin MyPin = new Pin();

        public AgendaPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

			#region map_UI_settings

			Map.UiSettings.CompassEnabled = true;
			Map.UiSettings.ZoomControlsEnabled = true;
			Map.UiSettings.ZoomGesturesEnabled = true;
			Map.UiSettings.IndoorLevelPickerEnabled = true;
			Map.UiSettings.TiltGesturesEnabled = true;

            EndDatePicker.MinimumDate = StartDatePicker.Date;
            StartDatePicker.DateSelected += (sender, e) => 
            {
                EndDatePicker.MinimumDate = e.NewDate;
            };

			#endregion
		}

        // Set Menu and display last known location 
        protected override void OnAppearing()
        {
            base.OnAppearing();

            SetMenu(MenuLayout, 4);           
          
            GetLastCachedLocation();
        }

        // Disconnect set pins event 
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ViewModel.PropertyChanged += null;
        }

        async void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var index = ViewModel.CourseItems.IndexOf(e.Item as CourseAgendaCellModel);
            await Map.AnimateCamera(CameraUpdateFactory.NewPositionZoom((Map.Pins[index].Position),16),new TimeSpan(0,0,2));
            listview.SelectedItem = null;
        }

        // Assign on set pins event whenever list is changed
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext == null)
                return;
            
            ViewModel = BindingContext as AgendaPageModel;

            ViewModel.PropertyChanged += (sender, e) => 
            {
                if(e.PropertyName == "CourseItems")
                {
                    SetPins(ViewModel);
                }
            };

            SetPins(ViewModel);
        }

        void FilterTap(object sender, System.EventArgs e)
        {
            var filter_label = sender as Label;

            if (filter_label!=null && !ViewModel.SelectedFilter.Equals(filter_label.Text) )
            {
                FilterContainer.Children.Remove(Filter);
				var index = FilterContainer.Children.IndexOf(filter_label);
                FilterContainer.Children.Insert(index, Filter);
                ViewModel.SelectedFilter = filter_label.Text.Trim();               
            }
        }


        // My location to be displayed at first launch from cached location which is fast 
		async void GetLastCachedLocation()
		{
			try
			{
				if (CrossGeolocator.IsSupported && CrossGeolocator.Current.IsGeolocationEnabled)
				{

					var locator = CrossGeolocator.Current;

					var location = await locator.GetLastKnownLocationAsync();

					if (location != null)
					{
                        Map.Pins.Add( MyPin = new Pin()
						{
							Address = "",
							IsDraggable = false,
							Flat = true,
							Label = "Current Location",
							Type = PinType.SavedPin,
							IsVisible = true,
                            Icon = BitmapDescriptorFactory.FromView(new BindingPinView("Me")),
							Position = new Position(location.Latitude, location.Longitude)
						});

                        Map.InitialCameraUpdate = (CameraUpdateFactory.NewPositionZoom(new Position(location.Latitude, location.Longitude),12d));

                        await locator.StopListeningAsync();

					}
					else
					{
						Handle_MyLocationButtonClicked(null, null);
					}

				}
			}
			catch (Exception)
			{
			}

		}

        // My location button draw current marker
		async void Handle_MyLocationButtonClicked(object sender, Xamarin.Forms.GoogleMaps.MyLocationButtonClickedEventArgs e)
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				if (CrossGeolocator.IsSupported && CrossGeolocator.Current.IsGeolocationEnabled)
				{

					var locator = CrossGeolocator.Current;
					locator.DesiredAccuracy = 50;

					var location = await locator.GetPositionAsync(new TimeSpan(0, 0, 6));

					if (location != null)
					{
                        var context = BindingContext as AgendaPageModel;

                        if(Map.Pins.Contains(MyPin))
                        Map.Pins.Remove(MyPin);
                           						

                        Map.Pins.Add( MyPin = new Pin()
						{
							Address = "",
							IsDraggable = false,
							Flat = true,
							Label = "Current Location",
							Type = PinType.SavedPin,
							IsVisible = true,
                            Icon = BitmapDescriptorFactory.FromView(new BindingPinView("Me")),
							Position = new Position(location.Latitude, location.Longitude)
						});

                        await Map.AnimateCamera(CameraUpdateFactory.NewPositionZoom(new Position(location.Latitude, location.Longitude),12), new TimeSpan(0, 0, 3));
					}

					await locator.StopListeningAsync();

				}
				else
				{
					var response = await DisplayAlert(AppResources.Alert, AppResources.LocationEnableAlert, AppResources.Ok, AppResources.NotNow);
				}
			}
			catch (Exception)
			{
			}

			IsBusy = false;
		}


        void SetPins(AgendaPageModel context)
		{

            if (context == null || context.CourseItems == null )
                return;
            
            Map.Pins.Clear();
            Map.Polylines.Clear();

            // Draw Point markers
            foreach (var item in context.CourseItems)
			{
				var pin = new Pin()
				{
                    Address = item.Address,
					IsDraggable = false,
					Flat = true,
                    Label = item.ContactName,
					IsVisible = true,
                    Icon = BitmapDescriptorFactory.FromView(new BindingPinView(item.Index)),
					Position = new Position(item.Latitude, item.Longitude)
				};
				Map.Pins.Add(pin);
			}

            // Draw Lines connecting points
            for (int i = 0; i < context.CourseItems.Count; i++)
            {
                if (i + 1 < context.CourseItems.Count)
                {
                    var item = context.CourseItems[i];
                    var next_item = context.CourseItems[i + 1];
                    var line = new Polyline() { IsClickable = false, StrokeColor = (Color)App.Current.Resources["darkSkyBlue"], StrokeWidth = 2 };
                    line.Positions.Add(new Position(item.Latitude,item.Longitude));
                    line.Positions.Add(new Position(next_item.Latitude,next_item.Longitude));
                    Map.Polylines.Add(line);
                }
            }

			if (!Map.Pins.Contains(MyPin) && !string.IsNullOrEmpty(MyPin.Label))
				Map.Pins.Add(MyPin);

		}


	}
}
