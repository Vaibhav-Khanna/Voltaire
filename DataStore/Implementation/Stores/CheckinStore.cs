using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Implementation.Stores
{
    public class CheckinStore : BaseStore<Checkin>, ICheckinStore
    {
        public override string Identifier => "Checkin";       
    }
}
