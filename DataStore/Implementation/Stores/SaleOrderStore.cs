using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Implementation.Stores
{
    public class SaleOrderStore : BaseStore<SaleOrder>, ISaleOrderStore
    {
        public override string Identifier => "SaleOrder";


        public async Task<IEnumerable<SaleOrder>> GetOrderItemsByCustomer(long PartnerId)
        {
            await InitializeStore().ConfigureAwait(false);

            return await Table.Where( x => x.PartnerId == PartnerId ).Where(x =>  x.State == QuotationStatus.sale.ToString() || x.State == QuotationStatus.done.ToString() ).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<SaleOrder>> GetQuotationItemsByCustomer(long PartnerId)
        {
            await InitializeStore().ConfigureAwait(false);

            return await Table.Where(x => x.PartnerId == PartnerId).Where(x => x.State == QuotationStatus.draft.ToString() || x.State == QuotationStatus.sent.ToString()).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
        }
    }
}
