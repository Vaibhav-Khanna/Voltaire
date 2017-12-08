using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Implementation.Stores
{
    public class ProductPriceListCountriesStore: BaseStore<ProductPriceList_Countries>, IProductPriceListCountriesStore
    {
        public override string Identifier => "ProductPriceList_Countries";
    }
}
