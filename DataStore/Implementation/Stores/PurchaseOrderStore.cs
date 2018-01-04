using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Implementation.Stores
{
    public class PurchaseOrderStore : BaseStore<PurchaseOrder>, IPurchaseOrderStore
    {
        public override string Identifier => "PurchaseOrder";
    }
}
