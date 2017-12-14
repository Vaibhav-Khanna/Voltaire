using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Implementation.Stores
{
    public class ProductStore : BaseStore<Product>, IProductStore
    {
        public override string Identifier => "Product";
    }
}
