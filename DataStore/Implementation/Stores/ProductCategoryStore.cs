using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Implementation.Stores
{
    public class ProductCategoryStore: BaseStore<ProductCategory>, IProductCategoryStore
    {
        public override string Identifier => "ProductCategory";
    }
}
