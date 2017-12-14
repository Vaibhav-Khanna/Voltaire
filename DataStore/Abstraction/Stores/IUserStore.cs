using System;
using System.Threading.Tasks;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Abstraction.Stores
{
    public interface IUserStore : IBaseStore<User>
    {
        Task<User> GetCurrentUserAsync();

        Task<User> GetUserByExternalIdAsync(long externalId);

    }
}
