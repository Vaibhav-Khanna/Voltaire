using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Implementation.Stores
{
    public class AccessoryStore: BaseStore<Accessory>, IAccessoryStore
    {
        public override string Identifier => "Accessory";
    }
}
