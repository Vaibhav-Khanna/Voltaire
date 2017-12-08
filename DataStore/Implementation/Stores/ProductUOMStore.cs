using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Implementation.Stores
{
    public class ProductUOMStore: BaseStore<ProductUOM>, IProductUOMStore
    {
        public override string Identifier => "ProductUOM";
    }
}
