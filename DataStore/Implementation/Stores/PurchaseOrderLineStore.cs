using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Implementation.Stores
{
    public class PurchaseOrderLineStore : BaseStore<PurchaseOrderLine>, IPurchaseOrderLineStore
    {
        public override string Identifier => "PurchaseOrderLine";
    }
}
