using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models;

namespace voltaire.DataStore.Implementation.Stores
{
    public class QuotationStore : BaseStore<Quotation>, IQuotationStore
    {       
        public override string Identifier => "Quotation";

        public async Task<IEnumerable<Quotation>> Search(string QueryText)
        {
            await InitializeStore().ConfigureAwait(false);
            await PullLatestAsync().ConfigureAwait(false);

            //TODO
            var items = await Table.IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);

            if (items == null)
                return null;

            return items;
        }

    }
}
