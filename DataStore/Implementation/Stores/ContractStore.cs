using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models;

namespace voltaire.DataStore.Implementation.Stores
{
    public class ContractStore : BaseStore<Contract>, IContractStore
    {
        public override string Identifier => "Contract";


        public async Task<IEnumerable<Contract>> Search(string QueryText)
        {
            await InitializeStore().ConfigureAwait(false);
            await PullLatestAsync().ConfigureAwait(false);

            //TODO
            var items = await Table.Where( s => s.Name.Contains(QueryText) || s.Subject.Contains(QueryText)).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);

            if (items == null)
                return null;

            return items;
        }

    }
}
