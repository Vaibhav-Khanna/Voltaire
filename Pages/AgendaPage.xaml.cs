using System;
using System.Collections.Generic;
using Plugin.Geolocator;
using voltaire.PageModels;
using voltaire.Resources;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace voltaire.Pages
{
    public partial class AgendaPage 
    {

        public AgendaPageModel ViewModel { get; set; } 


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

			#endregion
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            SetMenu(MenuLayout,4);
           
        }


        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            listview.SelectedItem = null;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext == null)
                return;
            
            ViewModel = BindingContext as AgendaPageModel;

            GetLastCachedLocation();

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
						Map.Pins.Add(new Pin()
						{
							Address = "",
							IsDraggable = false,
							Flat = true,
							Label = "Current Location",
							Type = PinType.SavedPin,
							IsVisible = true,
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
						var context = BindingContext as MapMainPageModel;

						Map.Pins.Clear();

						//if (context != null && context.CustomerAddresses != null)
						//{
						//	SetPins(context);
						//}

						Map.Pins.Add(new Pin()
						{
							Address = "",
							IsDraggable = false,
							Flat = true,
							Label = "Current Location",
							Type = PinType.SavedPin,
							IsVisible = true,
							Position = new Position(location.Latitude, location.Longitude)
						});

						await Map.AnimateCamera(CameraUpdateFactory.NewPosition(new Position(location.Latitude, location.Longitude)), new TimeSpan(0, 0, 3));
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


        void SetPins(MapMainPageModel context)
		{
			Map.Pins.Clear();

			foreach (var item in context.CustomerAddresses)
			{
				var pin = new Pin()
				{
					Address = item.Address,
					IsDraggable = false,
					Flat = true,
					Label = item.Title,
					Type = PinType.SavedPin,
					IsVisible = true,
					Position = new Position(item.Latitude, item.Longitude)
				};
				Map.Pins.Add(pin);
			}
		}


	}
}
