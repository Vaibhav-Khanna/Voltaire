using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Implementation.Stores
{
    public class ProductPriceListItemStore : BaseStore<ProductPriceListItem>, IProductPriceListItemStore
    {
        public override string Identifier => "ProductPriceListItem";
    }
}
