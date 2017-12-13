using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Implementation.Stores
{
    public class EventAlarmStore: BaseStore<EventAlarm>, IEventAlarmStore
    {
        public override string Identifier => "EventAlarm";
    }
}
