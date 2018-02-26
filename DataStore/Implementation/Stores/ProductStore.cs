using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;
using System.Linq;

namespace voltaire.DataStore.Implementation.Stores
{
    public class ProductStore : BaseStore<Product>, IProductStore
    {
        public override string Identifier => "Product";

        public Task<Product> GetItemsByProductId(long ProductId)
        {
            return null;
            //await InitializeStore().ConfigureAwait(false);

            //var items = await Table.Where(x => x.ExternalId == ProductId ).ToEnumerableAsync().ConfigureAwait(false);

            //if (items != null && items.Any())
            //    return items.FirstOrDefault();
            //else
                //return null;
        }

    }
}
