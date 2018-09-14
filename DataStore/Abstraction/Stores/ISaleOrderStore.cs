using System;
using voltaire.Models.DataObjects;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace voltaire.DataStore.Abstraction.Stores
{
    public interface ISaleOrderStore : IBaseStore<SaleOrder>
    {
        Task<IEnumerable<SaleOrder>> GetQuotationItemsByCustomer(long PartnerId);

        Task<IEnumerable<SaleOrder>> GetOrderItemsByCustomer(long PartnerId);

        Task<IEnumerable<SaleOrder>> GetQuotations(int currentCount);

        Task<IEnumerable<SaleOrder>> GetOrders(int currentCount);
    }
}
