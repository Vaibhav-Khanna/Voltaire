using System;
using voltaire.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace voltaire.DataStore.Abstraction.Stores
{
	public interface IContractStore : IBaseStore<Contract>
    {
        Task<IEnumerable<Contract>> GetContractsByPartnerExternalId(long id);

        Task<IEnumerable<string>> GetTermsConditionsOfContract();

        Task<string> GetContractTemplate();
    }
}
