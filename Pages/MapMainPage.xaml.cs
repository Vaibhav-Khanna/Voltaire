using System;
using System.Collections.Generic;
using Plugin.Geolocator;
using voltaire.PageModels;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using voltaire.Resources;
using voltaire.Controls;

namespace voltaire.Pages
{
    public partial class MapMainPage 
    {

        public MapMainPageModel ViewModel { get; set; }
		private Pin MyPin = new Pin();

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

			GetLastCachedLocation();
        }

		// Disconnect set pins event 
		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			ViewModel.PropertyChanged += null;
		}


        void Handle_Clicked(object sender, System.EventArgs e)
        {
            var button = sender as Button;

            W0.Style = (Style) App.Current.Resources["FilterWeightButtonStyle"];
			W1.Style = (Style)App.Current.Resources["FilterWeightButtonStyle"];
			W2.Style = (Style)App.Current.Resources["FilterWeightButtonStyle"];
			W3.Style = (Style)App.Current.Resources["FilterWeightButtonStyle"];
			W4.Style = (Style)App.Current.Resources["FilterWeightButtonStyle"];
			W5.Style = (Style)App.Current.Resources["FilterWeightButtonStyle"];
			
            button.Style = (Style) App.Current.Resources["FilterWeightClickedButtonStyle"];
        }

        void Grade_Clicked(object sender, System.EventArgs e)
        {
            ViewModel.PartnerGradeFilter.Execute((sender as Button).Text);
        }  

        void FilterReset(object sender, System.EventArgs e)
        {
			W0.Style = (Style)App.Current.Resources["FilterWeightClickedButtonStyle"];
			W1.Style = (Style)App.Current.Resources["FilterWeightButtonStyle"];
			W2.Style = (Style)App.Current.Resources["FilterWeightButtonStyle"];
			W3.Style = (Style)App.Current.Resources["FilterWeightButtonStyle"];
			W4.Style = (Style)App.Current.Resources["FilterWeightButtonStyle"];
			W5.Style = (Style)App.Current.Resources["FilterWeightButtonStyle"];
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
                        Map.Pins.Add(MyPin = new Pin()
						{
							Address = "",
							IsDraggable = false,
							Flat = true,
							Label = "Current Location",
							IsVisible = true,
                            Icon = BitmapDescriptorFactory.FromView(new BindingPinView("Me")),
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

            ViewModel = BindingContext as MapMainPageModel;

            if (ViewModel == null || ViewModel.Customers == null)
				return;

			#region Map_Pins_Set

			ViewModel.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName == "Customers")
				{
					SetPins(ViewModel);
				}
            };

            SetPins(ViewModel);

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

                        if(Map.Pins.Contains(MyPin))
                        Map.Pins.Remove(MyPin);

                        Map.Pins.Add(MyPin = new Pin()
                        {
                            Address = "",
                            IsDraggable = false,
							Flat = true,
							Label = "Current Location",							
							IsVisible = true,
                            Icon = BitmapDescriptorFactory.FromView(new BindingPinView("Me")),
                            Position = new Position(location.Latitude, location.Longitude)
						});

                        await Map.AnimateCamera(CameraUpdateFactory.NewPositionZoom(new Position(location.Latitude,location.Longitude),12),new TimeSpan(0,0,3));
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

            foreach (var cust in context.Customers)
			{
                foreach (var item in cust.CustomerAddresses)
                {
					var pin = new Pin()
					{
						Address = item.Address,
						IsDraggable = false,
						Flat = true,
						Label = item.Title,
						Type = PinType.SavedPin,
						IsVisible = true,
						Icon = BitmapDescriptorFactory.FromView(new BindingPinView("C")),
						Position = new Position(item.Latitude, item.Longitude)
					};
					Map.Pins.Add(pin);
                }
              
			}

            if (!Map.Pins.Contains(MyPin) && !string.IsNullOrEmpty(MyPin.Label))
                Map.Pins.Add(MyPin);
            
        }

    }
}
