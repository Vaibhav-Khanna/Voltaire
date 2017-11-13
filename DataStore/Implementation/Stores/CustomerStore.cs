using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models;

namespace voltaire.DataStore.Implementation.Stores
{
    public class CustomerStore :  BaseStore<Partner>, ICustomerStore
    {
        
        public override string Identifier => "Partner";

        public override async Task<IEnumerable<Partner>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeStore().ConfigureAwait(false);
            if (forceRefresh)
                await PullLatestAsync().ConfigureAwait(false);

            return await Table.OrderBy( x=> x.Name ).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
        }

        public override async Task<IEnumerable<Partner>> GetNextItemsAsync(int currentitemCount)
        {
            await InitializeStore().ConfigureAwait(false);

            try
            {
                return await Table.OrderBy(x => x.Name).Skip(currentitemCount).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Partner>> Search(string QueryText)
        {
            await InitializeStore().ConfigureAwait(false);           
          
            var items = await OnlineTable.Where(s => s.Name.Contains(QueryText) || s.CompanyName.Contains(QueryText)).OrderBy( x => x.Name ).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);

            if (items == null)
                return null;

            return items;
        }

    }
}
