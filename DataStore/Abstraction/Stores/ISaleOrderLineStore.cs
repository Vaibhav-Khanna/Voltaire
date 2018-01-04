using System;
using voltaire.Models.DataObjects;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace voltaire.DataStore.Abstraction.Stores
{
    public interface ISaleOrderLineStore : IBaseStore<SaleOrderLine>
    {
        Task<IEnumerable<SaleOrderLine>> GetItemsByOrderId(long OrderId);
    }
}
