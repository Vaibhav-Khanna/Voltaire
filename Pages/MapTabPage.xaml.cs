using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using voltaire.Controls.Items;
using voltaire.PageModels;
using voltaire.Pages.Base;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace voltaire.Pages
{
    public partial class MapTabPage : BaseViewPagerPage
    {
        public MapTabPage()
        {
            InitializeComponent();


            #region map_UI_settings

            map.UiSettings.CompassEnabled = true;
            map.UiSettings.MyLocationButtonEnabled = true;
            map.UiSettings.ZoomControlsEnabled = true;
            map.UiSettings.ZoomGesturesEnabled = true;

            #endregion

        }

        protected override void BindingContextSet()
        {
            base.BindingContextSet();

            var context = BindingContext as TTab;

            if (context == null || context.Customer.CustomerAddresses == null)
                return;


            #region Map_Pins_Set

            if (context.Customer.CustomerAddresses.Count != 0)
            {
                var pin = context.Customer.CustomerAddresses[0];
                map.InitialCameraUpdate = CameraUpdateFactory.NewPositionZoom(new Position(pin.Latitude, pin.Longitude), 12d);
            }

            foreach (var item in context.Customer.CustomerAddresses)
            {
                var pin = new Pin()
                {
                    Address = item.Address,
                    IsDraggable = true,
                    Flat = true,
                    Label = item.Title,
                    Type = PinType.SavedPin,
                    IsVisible = true,
                    Position = new Position(item.Latitude, item.Longitude)
                };
                map.Pins.Add(pin);
            }

            #endregion

        }
    }
}
