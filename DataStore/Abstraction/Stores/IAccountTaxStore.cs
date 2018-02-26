using System;
using System.Threading.Tasks;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Abstraction.Stores
{
    public interface IAccountTaxStore : IBaseStore<AccountTax>
    {
        Task<AccountTax> GetItemsByExternalId(long Id);
    }
}
