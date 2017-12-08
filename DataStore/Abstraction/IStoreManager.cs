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
        IProductPriceListCountriesStore ProductPriceListCountriesStore { get; }
        IProductPriceListItemStore ProductPriceListItemStore { get; }
        IProductPriceListStore ProductPriceListStore { get; }
        IProductUOMStore ProductUOMStore { get; }
        IPurchaseOrderLineStore PurchaseOrderLineStore { get; }
        IPurchaseOrderStore PurchaseOrderStore { get; }


        Task<bool> SyncAllAsync(bool syncUserSpecific);
        Task DropEverythingAsync();
    }
}
