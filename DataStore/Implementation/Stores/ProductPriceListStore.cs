using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Implementation.Stores
{
    public class ProductPriceListStore: BaseStore<ProductPriceList>, IProductPriceListStore
    {
        public override string Identifier => "ProductPriceList";
    }
}
