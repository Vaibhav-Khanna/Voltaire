using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models;

namespace voltaire.DataStore.Implementation.Stores
{
    public class QuotationStore : BaseStore<Quotation>, IQuotationStore
    {       
        public override string Identifier => "Quotation";
    }
}
