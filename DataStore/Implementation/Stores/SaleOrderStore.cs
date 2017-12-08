using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Implementation.Stores
{
    public class SaleOrderStore : BaseStore<SaleOrder>, ISaleOrderStore
    {
        public override string Identifier => "SaleOrder";
    }
}
