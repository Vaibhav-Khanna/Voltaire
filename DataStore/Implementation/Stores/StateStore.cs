using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Implementation.Stores
{
    public class StateStore: BaseStore<State>, IStateStore
    {
        public override string Identifier => "State";

        public async Task<State> GetStateByExternalId(long Id)
        {
            await InitializeStore().ConfigureAwait(false);

            try
            {
                var items = await Table.Where(x => x.ExternalId == Id).ToEnumerableAsync().ConfigureAwait(false);

                if (items != null && items.Any())
                {
                    return items.First();
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<State>> Search(string QueryText)
        {
            await InitializeStore().ConfigureAwait(false);

            IEnumerable<State> items;

            items = await Table.Where(s => s.Name.Contains(QueryText)).OrderBy(x => x.Name).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);

            return items;
        }

    }
}
