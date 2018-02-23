using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Implementation.Stores
{
    public class SaleOrderLineStore : BaseStore<SaleOrderLine>, ISaleOrderLineStore
    {
        public override string Identifier => "SaleOrderLine";

        public async Task<IEnumerable<SaleOrderLine>> GetItemsByOrderId(string OrderId)
        {
            await InitializeStore().ConfigureAwait(false);

            return await Table.Where(x => x.OrderId == OrderId).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
        }
    }
}
