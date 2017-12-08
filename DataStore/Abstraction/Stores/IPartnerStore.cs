using System;
using voltaire.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace voltaire.DataStore.Abstraction.Stores
{
    public interface IPartnerStore : IBaseStore<Partner>
    {
        Task<IEnumerable<Partner>> Search(string QueryText);
    }
}
