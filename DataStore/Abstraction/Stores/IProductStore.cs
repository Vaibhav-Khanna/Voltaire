using System;
using voltaire.Models.DataObjects;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace voltaire.DataStore.Abstraction.Stores
{
    public interface IProductStore : IBaseStore<Product>
    {
        Task<Product> GetItemsByProductId(long ProductId);
    }
}
