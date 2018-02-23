using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Implementation.Stores
{
    public class ProductTemplateStore : BaseStore<ProductTemplate>, IProductTemplateStore
    {
        public override string Identifier => "ProductTemplate";
    }
}
