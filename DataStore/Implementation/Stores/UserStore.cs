using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FreshMvvm;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;
using voltaire.PageModels;
using System.Linq;
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
                return null;              
            }

            var id = StoreManager.MobileService.CurrentUser.UserId;

            if (id != null)
            {
                try
                {
                    await InitializeStore().ConfigureAwait(false);

                    var items = await Table.Where(s => s.PartnerId == id).ToListAsync().ConfigureAwait(false);

                    if (items == null || !items.Any())
                        return null;

                    currentUser = items[0];

                    return currentUser;

                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
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
