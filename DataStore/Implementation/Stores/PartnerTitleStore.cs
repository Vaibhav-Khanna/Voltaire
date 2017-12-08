using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Implementation.Stores
{
    public class PartnerTitleStore : BaseStore<PartnerTitle>, IPartnerTitleStore
    {
        public override string Identifier => "PartnerTitle";
    }
}
