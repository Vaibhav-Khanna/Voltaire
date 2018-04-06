using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Implementation.Stores
{
    public class StateStore: BaseStore<State>, IStateStore
    {
        public override string Identifier => "State";

        public async Task<IEnumerable<State>> Search(string QueryText)
        {
            await InitializeStore().ConfigureAwait(false);

            IEnumerable<State> items;

            items = await Table.Where(s => s.Name.Contains(QueryText)).OrderBy(x => x.Name).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);

            return items;
        }

    }
}
