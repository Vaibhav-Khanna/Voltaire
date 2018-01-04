using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Implementation.Stores
{
    public class CountryStore : BaseStore<Country>, ICountryStore
    {
        public override string Identifier => "Country";
    }
}
