using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Implementation.Stores
{
    public class CurrencyStore : BaseStore<Currency>, ICurrencyStore
    {
        public override string Identifier => "Currency";
    }
}
