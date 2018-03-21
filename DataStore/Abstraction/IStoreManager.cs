using System;
using System.Threading.Tasks;
using voltaire.DataStore.Abstraction.Stores;

namespace voltaire.DataStore.Abstraction
{
    public interface IStoreManager
    {
        bool IsInitialized { get; }
        Task InitializeAsync();


        ICountryStore CountryStore { get; }
        ICurrencyStore CurrencyStore { get; }
        IPartnerCategoryStore PartnerCategoryStore { get; }
        IPartnerGradeStore PartnerGradeStore { get; }
        IPartnerStore CustomerStore { get; }
        IPartnerTitleStore PartnerTitleStore { get; }
        IUserStore UserStore { get; }      
        ISaleOrderStore SaleOrderStore { get; }
        ISaleOrderLineStore SaleOrderLineStore { get; } 
        IMessageStore MessageStore { get; } 
        ICheckinStore CheckinStore { get; }
        IDocumentStore DocumentStore { get; }
        ICompanyStore CompanyStore { get; }
        IStateStore StateStore { get; }
        IAccountTaxStore AccountTaxStore { get; }
        IAccessoryStore AccessoryStore { get; }
        ISaddlePriceStore SaddleStore { get; }
        IServiceStore ServiceStore { get; }

        Task<bool> SyncAllAsync(bool syncUserSpecific);
        Task DropEverythingAsync();
    }
}
