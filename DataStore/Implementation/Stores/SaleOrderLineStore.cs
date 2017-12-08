using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Implementation.Stores
{
    public class SaleOrderLineStore : BaseStore<SaleOrderLine>, ISaleOrderLineStore
    {
        public override string Identifier => "SaleOrderLine";
    }
}
