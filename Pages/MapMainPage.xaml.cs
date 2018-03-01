using System;
using System.Collections.Generic;
using Plugin.Geolocator;
using voltaire.PageModels;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using voltaire.Resources;
using voltaire.Controls;
using System.Diagnostics;

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
            Map.MyLocationEnabled = true;
            Map.UiSettings.MyLocationButtonEnabled = true;
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
                            Icon = BitmapDescriptorFactory.FromView(new BindingPinView("Me",Color.Transparent)),
							Position = new Position(location.Latitude, location.Longitude)
						});

                        Map.InitialCameraUpdate = (CameraUpdateFactory.NewPositionZoom(new Position(location.Latitude, location.Longitude),1d));

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

            if (ViewModel == null)
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
                            Icon = BitmapDescriptorFactory.FromView(new BindingPinView("Me",Color.Transparent)),
                            Position = new Position(location.Latitude, location.Longitude)
						});

                        await Map.AnimateCamera(CameraUpdateFactory.NewPositionZoom(new Position(location.Latitude,location.Longitude),1),new TimeSpan(0,0,3));
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

            if(context.Customers!=null)
            foreach (var cust in context.Customers)
            {
                if (cust.PartnerLatitude != 0 && cust.PartnerLongitude != 0)
                {
                    var pin = new Pin()
                    {
                        Address = cust.ContactAddress,
                        IsDraggable = false,
                        Flat = true,
                        Label = cust.Name,
                        Type = PinType.SavedPin,
                        IsVisible = true,
                        Icon = BitmapDescriptorFactory.FromView(new BindingPinView( string.IsNullOrWhiteSpace(cust.Name) ? "P" : cust.Name.Trim().Substring(0,1),Convert(cust.LastCheckinAt))),
                        Position = new Position(cust.PartnerLatitude, cust.PartnerLongitude)
                    };
                    Map.Pins.Add(pin);
                }
            }

            if (!Map.Pins.Contains(MyPin) && !string.IsNullOrEmpty(MyPin.Label))
                Map.Pins.Add(MyPin);
        }


        void Handle_Clicked(object sender, System.EventArgs e)
        {
            var normal_style = (Style)App.Current.Resources["FilterWeightButtonStyle"];
            bt0.Style = normal_style;
            bt1.Style = normal_style;
            bt2.Style = normal_style;
            bt3.Style = normal_style;
            bt4.Style = normal_style;
            bt5.Style = normal_style;

            (sender as Button).Style = (Style)App.Current.Resources["FilterWeightClickedButtonStyle"];
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            var normal_style = (Style)App.Current.Resources["FilterWeightButtonStyle"];
            bt0.Style = normal_style;
            bt1.Style = normal_style;
            bt2.Style = normal_style;
            bt3.Style = normal_style;
            bt4.Style = normal_style;
            bt5.Style = normal_style;

            var frame_style = (Style)App.Current.Resources["PartnerGradeFrame"];
            var button_style = (Style)App.Current.Resources["PartnerGradeButton"];

            foreach (var item in grades.Children)
            {
                (item as Frame).Style = frame_style;
                ((item as Frame).Content as Button).Style = button_style;
            }

        }

        void Handle_Grade(object sender, System.EventArgs e)
        {
            var frame_style = (Style)App.Current.Resources["PartnerGradeFrame"];
            var button_style = (Style)App.Current.Resources["PartnerGradeButton"];

            foreach (var item in grades.Children)
            {
                (item as Frame).Style = frame_style;
                ((item as Frame).Content as Button).Style = button_style;
            }


            ((sender as Button)).Style = (Style)App.Current.Resources["PartnerGradeButtonSelected"];

            ((sender as Button).Parent as Frame).Style = (Style)App.Current.Resources["PartnerGradeFrameSelected"];

            (BindingContext as MapMainPageModel).PartnerGradeFilter.Execute((sender as Button).Text);

        }

        public Color Convert(DateTime? item)
        {
           
            DateTime date;

            if (item.HasValue)
                date = item.Value;
            else
                return Color.Transparent; // red

            if (DateTime.Now.Subtract(date).Days <= 7)
            {
                return Color.FromHex("13c10d"); // green
            }
            else if (DateTime.Now.Subtract(date).Days <= 30)
            {
                return Color.FromHex("fc9835"); // orange
            }
            else if (DateTime.Now.Subtract(date).Days > 30)
            {
                return  Color.FromHex("eb1010");  // red
            }
            else
                return Color.Transparent; // red

        }

    }
}
