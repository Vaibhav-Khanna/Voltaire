using System;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Abstraction.Stores
{
    public interface IEventStore : IBaseStore<Event>
    {
      
    }
}
