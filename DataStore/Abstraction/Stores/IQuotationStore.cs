using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using voltaire.Models;

namespace voltaire.DataStore.Abstraction.Stores
{
    public interface IQuotationStore : IBaseStore<Quotation>
    {
        Task<IEnumerable<Quotation>> Search(string QueryText);
    }
}
