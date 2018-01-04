using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Implementation.Stores
{
    public class PartnerCategoryStore : BaseStore<PartnerCategory>, IPartnerCategoryStore
    {
         public override string Identifier => "PartnerCategory";
    }
}
