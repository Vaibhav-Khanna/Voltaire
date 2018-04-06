using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Implementation.Stores
{
    public class CountryStore : BaseStore<Country>, ICountryStore
    {
        public override string Identifier => "Country";

      

        public async Task<IEnumerable<Country>> Search(string QueryText)
        {
            await InitializeStore().ConfigureAwait(false);

            IEnumerable<Country> items;
                
            items = await Table.Where(s => s.Name.Contains(QueryText)).OrderBy(x => x.Name).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);

            return items;
        }
    }
}
