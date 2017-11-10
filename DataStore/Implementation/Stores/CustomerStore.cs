using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models;

namespace voltaire.DataStore.Implementation.Stores
{
    public class CustomerStore :  BaseStore<Partner>, ICustomerStore
    {
        public override string Identifier => "Partner";
    }
}
