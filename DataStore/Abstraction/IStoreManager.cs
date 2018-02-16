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
        IProductStore ProductStore { get; }
        IProductPriceListItemStore ProductPriceListItemStore { get; }
        IProductPriceListStore ProductPriceListStore { get; }
        IProductUOMStore ProductUOMStore { get; }
        IPurchaseOrderLineStore PurchaseOrderLineStore { get; }
        IPurchaseOrderStore PurchaseOrderStore { get; }
        ISaleOrderStore SaleOrderStore { get; }
        ISaleOrderLineStore SaleOrderLineStore { get; }
        IEventStore EventStore { get; }
        IEventAlarmStore EventAlarmStore { get; }
        IMessageStore MessageStore { get; }
        IAccessoryStore AccessoryStore { get; }
        IAccessoryCategoryStore AccessoryCategoryStore { get; }
        ISaddlePriceStore SaddlePriceStore { get; }
        IServiceStore ServiceStore { get; }
        ICheckinStore CheckinStore { get; }
        IDocumentStore DocumentStore { get; }
        ICompanyStore CompanyStore { get; }

        Task<bool> SyncAllAsync(bool syncUserSpecific);
        Task DropEverythingAsync();
    }
}
