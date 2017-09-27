using System;
using System.Collections.Generic;
using Plugin.Geolocator;
using voltaire.PageModels;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using voltaire.Resources;

namespace voltaire.Pages
{
    public partial class MapMainPage 
    {


        public MapMainPage()
        {
            NavigationPage.SetHasNavigationBar(this,false);
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

            SetMenu(MenuLayout,2);
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

                    }
                    else
                    {
                        Handle_MyLocationButtonClicked(null, null);
                    }

                }
            }
            catch(Exception)
            {                
            }

        }


        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

			var context = BindingContext as MapMainPageModel;

			if (context == null || context.CustomerAddresses == null)
				return;

			#region Map_Pins_Set

			if (context.CustomerAddresses.Count != 0)
			{
				var pin = context.CustomerAddresses[0];
				Map.InitialCameraUpdate = CameraUpdateFactory.NewPositionZoom(new Position(pin.Latitude, pin.Longitude), 12d);
			}

            SetPins(context);

            GetLastCachedLocation();

			#endregion

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

                    if(location!=null)
                    {
                        var context = BindingContext as MapMainPageModel;

                        Map.Pins.Clear();

                        if(context !=null && context.CustomerAddresses!= null)
                        {
                            SetPins(context);
                        }

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

                        await Map.AnimateCamera(CameraUpdateFactory.NewPosition(new Position(location.Latitude,location.Longitude)),new TimeSpan(0,0,3));
                    }

                    await locator.StopListeningAsync();

                }
                else
                {
                    var response = await DisplayAlert(AppResources.Alert, AppResources.LocationEnableAlert, AppResources.Ok, AppResources.NotNow);
                }
            }
            catch(Exception)
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
