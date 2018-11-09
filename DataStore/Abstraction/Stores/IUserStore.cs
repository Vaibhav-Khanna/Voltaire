using System;
using System.Threading.Tasks;
using voltaire.Models.DataObjects;
using System.Collections.Generic;
using voltaire.Models;

namespace voltaire.DataStore.Abstraction.Stores
{
    public interface IUserStore : IBaseStore<User>
    {
        Task<User> GetCurrentUserAsync();

        Task<User> GetUserByExternalIdAsync(long externalId);

        Task<List<UserSale>> GetSalesForMonth(int month, int year, bool IsSales);
    }
}
