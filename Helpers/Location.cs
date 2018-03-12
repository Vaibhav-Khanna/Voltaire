using System;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

namespace voltaire.Helpers
{
    public static class Location
    {
        public static async Task<Position> GetCurrentLocation(bool force)
        {
            Position position = null;

            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;

                if (!force)
                {
                    position = await locator.GetLastKnownLocationAsync();
                }

                if (position != null)
                {
                    //got a cahched position, so let's use it.
                    return position;
                }

                if(!IsLocationAvailable())
                {
                    return null;
                }

                if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                {
                    //not available or enabled
                    return null;
                }

                position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, false);

            }
            catch (Exception ex)
            {
                //Display error as we have timed out or can't get location.
            }

            if (position == null)
                return null;

            var output = string.Format("Time: {0} \nLat: {1} \nLong: {2} \nAltitude: {3} \nAltitude Accuracy: {4} \nAccuracy: {5} \nHeading: {6} \nSpeed: {7}",
                position.Timestamp, position.Latitude, position.Longitude,
                position.Altitude, position.AltitudeAccuracy, position.Accuracy, position.Heading, position.Speed);

            return position;
        }


        public static bool IsLocationAvailable()
        {
            if (!CrossGeolocator.IsSupported)
                return false;

            return CrossGeolocator.Current.IsGeolocationAvailable;
        }
    }
}
