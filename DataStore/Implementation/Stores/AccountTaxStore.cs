using System;
using System.Threading.Tasks;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;
using System.Linq;

namespace voltaire.DataStore.Implementation.Stores
{
    public class AccountTaxStore : BaseStore<AccountTax>, IAccountTaxStore
    {
        public override string Identifier => "AccountTax";

        public async Task<AccountTax> GetItemsByExternalId(long Id)
        {
            await InitializeStore();

            var items = await Table.Where( x => x.ExternalId == Id ).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);

            if (items == null || !items.Any())
                return null;

            return items.First();
        }
    }
}
