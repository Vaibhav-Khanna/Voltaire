using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Implementation.Stores
{
    public class StateStore: BaseStore<State>, IStateStore
    {
        public override string Identifier => "State";

    }
}
