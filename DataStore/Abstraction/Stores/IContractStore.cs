using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using voltaire.Models;

namespace voltaire.DataStore.Abstraction.Stores
{
    public interface IContractStore : IBaseStore<Contract>
    {
        Task<IEnumerable<Contract>> Search(string QueryText);
    }
}
