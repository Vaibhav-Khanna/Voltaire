using System;
using System.Threading.Tasks;
using voltaire.DataStore.Abstraction.Stores;

namespace voltaire.DataStore.Abstraction
{
    public interface IStoreManager
    {
        bool IsInitialized { get; }
        Task InitializeAsync();
      
        ICustomerStore CustomerStore { get; }       
        IQuotationStore QuotationStore { get; }
        IContractStore ContractStore { get; }

        Task<bool> SyncAllAsync(bool syncUserSpecific);
        Task DropEverythingAsync();
    }
}
