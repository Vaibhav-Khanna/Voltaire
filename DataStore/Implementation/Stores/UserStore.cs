using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FreshMvvm;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;
using voltaire.PageModels;
using Xamarin.Forms;

namespace voltaire.DataStore.Implementation.Stores
{
    public class UserStore : BaseStore<User>, IUserStore
    {
        public override string Identifier => "User";

        public User currentUser;

        public async Task<User> GetCurrentUserAsync()
        {

            if (currentUser != null)
                return currentUser;

            if (StoreManager.MobileService.CurrentUser == null)
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    var homePage = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();

                    App.Current.MainPage = new FreshNavigationContainer(homePage) { BarTextColor = Color.Black };
                });
            }

            var id = StoreManager.MobileService.CurrentUser.UserId;

            try
            {
                await InitializeStore().ConfigureAwait(false);

                //var item = await Table.LookupAsync(id).ConfigureAwait(false);

                var items = await Table.Where(s => s.ExternalId.ToString() == id).ToListAsync().ConfigureAwait(false);

                if (items == null || items.Count == 0)
                    return null;

                currentUser = items[0];

                return currentUser;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual async Task<User> GetUserByExternalIdAsync(long externalId)
        {
            try
            {
                await InitializeStore().ConfigureAwait(false);
                await PullLatestAsync().ConfigureAwait(false);
                var items = await Table.Where(s => s.ExternalId == externalId).ToListAsync().ConfigureAwait(false);

                if (items == null || items.Count == 0)
                    return null;

                return items[0];
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
