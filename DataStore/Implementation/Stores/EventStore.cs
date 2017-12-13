using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Implementation.Stores
{
    public class EventStore: BaseStore<Event>, IEventStore
    {
        public override string Identifier => "Event";
    }
}
