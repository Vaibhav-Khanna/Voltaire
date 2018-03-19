using System;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace voltaire.Helpers
{
    public static class Permissions
    {

        public static async Task<bool> CheckPermissionLocation()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);

                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.LocationWhenInUse))
                    {
                        await App.Current.MainPage.DisplayAlert("Accès à la location", "L'application nécessite l'accès à la location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.LocationWhenInUse);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.LocationWhenInUse))
                        status = results[Permission.LocationWhenInUse];
                }

                if (status == PermissionStatus.Granted)
                {
                    return true;
                }
                else
                {
                    var result = await App.Current.MainPage.DisplayAlert("Accès à la location", "Vous avez refusé l'accès à la location. Nous ne pouvons continuer.", "Settings", "Maybe Later");

                    if (result)
                        CrossPermissions.Current.OpenAppSettings();

                    return false;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
