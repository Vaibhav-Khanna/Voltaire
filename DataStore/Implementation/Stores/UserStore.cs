using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Implementation.Stores
{
    public class UserStore: BaseStore<User>, IUserStore
    {
        public override string Identifier => "ProductPriceList_Countries";
    }
}
