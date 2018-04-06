using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Abstraction.Stores
{
    public interface ICountryStore : IBaseStore<Country>
    {
        Task<IEnumerable<Country>> Search(string QueryText);
    }
}
