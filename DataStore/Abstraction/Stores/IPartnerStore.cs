using System;
using voltaire.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace voltaire.DataStore.Abstraction.Stores
{
    public interface IPartnerStore : IBaseStore<Partner>
    {
        Task<Partner> GetCustomerByMessageAuthorIdAsync(string messageAuthorId);

        Task<Partner> GetItemByExternalId(long id);

        Task<IEnumerable<Partner>> GetItemsAsync(int? Weight, long? Grade, bool forceRefresh = false);

        Task<IEnumerable<Partner>> GetNextItemsAsync(int currentitemCount, int? Weight, long? Grade, bool forceRefresh = false);

        Task<IEnumerable<Partner>> Search(string QueryText, int? Weight, long? Grade, int CurrentItems = 0);

        Task<IEnumerable<Partner>> GetItemsWithValidCordinates();
    }
}
