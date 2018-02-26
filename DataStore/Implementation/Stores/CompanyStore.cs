using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Implementation.Stores
{
    public class CompanyStore : BaseStore<Company> , ICompanyStore
    {
        public override string Identifier => "Company";     
    }
}
