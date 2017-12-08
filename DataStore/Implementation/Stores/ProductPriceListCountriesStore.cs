using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Implementation.Stores
{
    public class ProductPriceListCountriesStore: BaseStore<ProductPriceListCountries>, IProductPriceListCountriesStore
    {
        public override string Identifier => "ProductPriceListCountries";
    }
}
