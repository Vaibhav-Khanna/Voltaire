using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Abstraction.Stores
{
    public interface IStateStore : IBaseStore<State>
    {
        Task<State> GetStateByExternalId(long Id);

        Task<IEnumerable<State>> Search(string QueryText);
    }
}
