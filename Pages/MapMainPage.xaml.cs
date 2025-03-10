﻿using System;
using System.Collections.Generic;
using Plugin.Geolocator;
using voltaire.PageModels;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using voltaire.Resources;
using voltaire.Controls;
using System.Diagnostics;
using System.Threading.Tasks;

namespace voltaire.Pages
{
    public partial class MapMainPage
    {

        public MapMainPageModel ViewModel { get; set; }
        private Pin MyPin = new Pin();

        public MapMainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
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

            SetMenu(MenuLayout, 2);

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
                var Has_Permission = await Helpers.Permissions.CheckPermissionLocation();

                if (!Has_Permission)
                    return;

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
                            Label = AppResources.CurrentLocation,
                            IsVisible = true,
                            Icon = BitmapDescriptorFactory.FromView(new BindingPinView(AppResources.Me, Color.Transparent)),
                            Position = new Position(location.Latitude, location.Longitude)
                        });

                        Map.InitialCameraUpdate = (CameraUpdateFactory.NewPositionZoom(new Position(location.Latitude, location.Longitude), 12d));
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
                    if (ViewModel.Customers != null && ViewModel.Customers.Count >= 0)
                    {
                        SetPins(ViewModel);
                    }
                }
                else if(e.PropertyName == "AllPartners")
                {
                    (BindingContext as MapMainPageModel).FilterVisibleRegion(Map.VisibleRegion);
                    bt_search.IsVisible = true;
                }
            };

            SetPins(ViewModel);

            #endregion
        }

        void Handle_CameraMoveStarted(object sender, Xamarin.Forms.GoogleMaps.CameraMoveStartedEventArgs e)
        {
            bt_search.IsVisible = true;
            bt_search.IsEnabled = false;
        }

        void Handle_CameraIdled(object sender, Xamarin.Forms.GoogleMaps.CameraIdledEventArgs e)
        {
            bt_search.IsEnabled = true;

            (BindingContext as MapMainPageModel).FilterVisibleRegion(Map.VisibleRegion);
        }

        void SearchAreaClicked(object sender, System.EventArgs e)
        {
            Map.IsEnabled = false;
            bt_search.IsEnabled = false;
            bt_search.Text = AppResources.Searching;

            var area_visible =  Map.VisibleRegion;

            (BindingContext as MapMainPageModel).FilterVisibleRegion(area_visible);

            Map.IsEnabled = true;
            bt_search.IsVisible = false;
            bt_search.Text = AppResources.SearchArea;
        }

        async void Handle_MyLocationButtonClicked(object sender, Xamarin.Forms.GoogleMaps.MyLocationButtonClickedEventArgs e)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            var Has_Permission = await Helpers.Permissions.CheckPermissionLocation();

            if (!Has_Permission)
            {
                IsBusy = false;
                return;
            }

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

                        if (Map.Pins.Contains(MyPin))
                            Map.Pins.Remove(MyPin);

                        Map.Pins.Add(MyPin = new Pin()
                        {
                            Address = "",
                            IsDraggable = false,
                            Flat = true,
                            Label = AppResources.CurrentLocation,
                            IsVisible = true,
                            Icon = BitmapDescriptorFactory.FromView(new BindingPinView(AppResources.Me, Color.Transparent)),
                            Position = new Position(location.Latitude, location.Longitude)
                        });

                        await Map.AnimateCamera(CameraUpdateFactory.NewPositionZoom(new Position(location.Latitude, location.Longitude), 12d), new TimeSpan(0, 0, 3));
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
            if ((Map.Pins != null) && (Map.Pins.Count > 0))
            {
                Map.Pins.Clear();
            }

            if (context.Customers != null)
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
                            Icon = BitmapDescriptorFactory.FromView(new BindingPinView(string.IsNullOrWhiteSpace(cust.Name) ? "P" : cust.Name.Trim().Substring(0, 1), Convert(cust.LastCheckinAt))),
                            Position = new Position(cust.PartnerLatitude.HasValue ? cust.PartnerLatitude.Value : 0, cust.PartnerLongitude.HasValue ? cust.PartnerLongitude.Value : 0)
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
                return Color.FromHex("eb1010");  // red
            }
            else
                return Color.Transparent; // red

        }

    }
}
