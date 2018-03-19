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

            map.UiSettings.CompassEnabled = false;
            map.UiSettings.MyLocationButtonEnabled = false;
            map.UiSettings.ZoomControlsEnabled = true;
            map.UiSettings.ZoomGesturesEnabled = true;

            #endregion

        }

        protected async override void BindingContextSet()
        {
            base.BindingContextSet();

            var context = BindingContext as TTab;

            if (context == null || context.Customer.PartnerLatitude == 0 || context.Customer.PartnerLongitude == 0)
                return;

            var Has_Permission = await Helpers.Permissions.CheckPermissionLocation();

            if (!Has_Permission)
            {
                return;
            }

            #region Map_Pins_Set

            map.InitialCameraUpdate = CameraUpdateFactory.NewPositionZoom(new Position(context.Customer.PartnerLatitude.HasValue ? context.Customer.PartnerLatitude.Value : 0, context.Customer.PartnerLongitude.HasValue ? context.Customer.PartnerLongitude.Value : 0), 12d);

            var pin = new Pin()
            {
                Address = context.Customer.ContactAddress,
                IsDraggable = true,
                Flat = true,
                Label = context.Customer.Name,
                Type = PinType.SavedPin,
                IsVisible = true,
                Position = new Position(context.Customer.PartnerLatitude.HasValue ? context.Customer.PartnerLatitude.Value : 0, context.Customer.PartnerLongitude.HasValue ? context.Customer.PartnerLongitude.Value : 0)
            };
            map.Pins.Add(pin);

            #endregion

        }
    }
}
