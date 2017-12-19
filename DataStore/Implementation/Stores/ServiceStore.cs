using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Implementation.Stores
{
    public class ServiceStore: BaseStore<Service>, IServiceStore
    {
        public override string Identifier => "Service";
    }
}
