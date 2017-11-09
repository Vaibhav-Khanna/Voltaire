using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models;

namespace voltaire.DataStore.Implementation.Stores
{
    public class ContractStore : BaseStore<Contract>, IContractStore
    {
        public override string Identifier => "Contract";
    }
}
