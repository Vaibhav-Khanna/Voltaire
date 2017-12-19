using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;
namespace voltaire.DataStore.Implementation.Stores
{
    public class AccessoryCategoryStore: BaseStore<AccessoryCategory>, IAccessoryCategoryStore
    {
        public override string Identifier => "AccessoryCategory";
    }
}
