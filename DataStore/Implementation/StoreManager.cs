using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Newtonsoft.Json.Linq;
using voltaire.DataStore.Abstraction;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models;
using System.Net.Http;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Text;
using System.Diagnostics;
using voltaire.Models.DataObjects;
using voltaire.DataStore.Implementation.Stores;

namespace voltaire.DataStore.Implementation
{
    public class StoreManager : IStoreManager
    {

        public static MobileServiceClient MobileService { get; set; }

        public bool IsInitialized { get; private set; }


        IPartnerStore customerStore;
        public IPartnerStore CustomerStore => customerStore ?? (customerStore = DependencyService.Get<IPartnerStore>());

        IPartnerCategoryStore partnerCategoryStore;
        public IPartnerCategoryStore PartnerCategoryStore => partnerCategoryStore ?? (partnerCategoryStore = DependencyService.Get<IPartnerCategoryStore>());

        ICountryStore countryStore;
        public ICountryStore CountryStore => countryStore ?? (countryStore = DependencyService.Get<ICountryStore>());

        ICurrencyStore currencyStore;
        public ICurrencyStore CurrencyStore => currencyStore ?? (currencyStore = DependencyService.Get<ICurrencyStore>());

        IPartnerGradeStore partnerGradeStore;
        public IPartnerGradeStore PartnerGradeStore => partnerGradeStore ?? (partnerGradeStore = DependencyService.Get<IPartnerGradeStore>());

        IPartnerTitleStore partnerTitleStore;
        public IPartnerTitleStore PartnerTitleStore => partnerTitleStore ?? (partnerTitleStore = DependencyService.Get<IPartnerTitleStore>());

        IUserStore userStore;
        public IUserStore UserStore => userStore ?? (userStore = DependencyService.Get<IUserStore>());

        IProductStore productStore;
        public IProductStore ProductStore => productStore ?? (productStore = DependencyService.Get<IProductStore>());

        IProductPriceListItemStore productPriceListItemStore;
        public IProductPriceListItemStore ProductPriceListItemStore => productPriceListItemStore ?? (productPriceListItemStore = DependencyService.Get<IProductPriceListItemStore>());

        IProductPriceListStore productPriceListStore;
        public IProductPriceListStore ProductPriceListStore => productPriceListStore ?? (productPriceListStore = DependencyService.Get<IProductPriceListStore>());

        IProductUOMStore productUOMStore;
        public IProductUOMStore ProductUOMStore => productUOMStore ?? (productUOMStore = DependencyService.Get<IProductUOMStore>());

        IPurchaseOrderLineStore purchaseOrderLineStore;
        public IPurchaseOrderLineStore PurchaseOrderLineStore => purchaseOrderLineStore ?? (purchaseOrderLineStore = DependencyService.Get<IPurchaseOrderLineStore>());

        IPurchaseOrderStore purchaseOrderStore;
        public IPurchaseOrderStore PurchaseOrderStore => purchaseOrderStore ?? (purchaseOrderStore = DependencyService.Get<IPurchaseOrderStore>());

        ISaleOrderStore saleOrderStore;
        public ISaleOrderStore SaleOrderStore => saleOrderStore ?? (saleOrderStore = DependencyService.Get<ISaleOrderStore>());

        ISaleOrderLineStore saleOrderLineStore;
        public ISaleOrderLineStore SaleOrderLineStore => saleOrderLineStore ?? (saleOrderLineStore = DependencyService.Get<ISaleOrderLineStore>());

        IEventStore eventStore;
        public IEventStore EventStore => eventStore ?? (eventStore = DependencyService.Get<IEventStore>());

        IEventAlarmStore eventAlarmStore;
        public IEventAlarmStore EventAlarmStore => eventAlarmStore ?? (eventAlarmStore = DependencyService.Get<IEventAlarmStore>());

        IMessageStore messageStore;
        public IMessageStore MessageStore => messageStore ?? (messageStore = DependencyService.Get<IMessageStore>());


        #region iStoreManager Implementation

        public Task DropEverythingAsync()
        {
            //Settings.UpdateDatabaseId();
            //TODO Do the update id for settings and add rest of the tables

            CustomerStore.DropTable();
            PartnerCategoryStore.DropTable();

            CountryStore.DropTable();
            CurrencyStore.DropTable();
            PartnerGradeStore.DropTable();
            PartnerTitleStore.DropTable();
            UserStore.DropTable();
            ProductStore.DropTable();
            ProductPriceListItemStore.DropTable();
            ProductPriceListStore.DropTable();
            ProductUOMStore.DropTable();
            PurchaseOrderLineStore.DropTable();
            PurchaseOrderStore.DropTable();
            SaleOrderStore.DropTable();
            SaleOrderLineStore.DropTable();
            EventStore.DropTable();
            EventAlarmStore.DropTable();
            MessageStore.DropTable();

            IsInitialized = false;
            return Task.FromResult(true);
        }


        object locker = new object();

        public async Task InitializeAsync()
        {
            MobileServiceSQLiteStore store;

            lock (locker)
            {

                if (IsInitialized)
                    return;

                IsInitialized = true;
                // var dbId = Settings.DatabaseId;
                // var path = $"syncstore{dbId}.db";
                MobileService = new MobileServiceClient("http://voltairecrm.azurewebsites.net");
                store = new MobileServiceSQLiteStore("syncstore.db");

                store.DefineTable<Partner>();
                store.DefineTable<PartnerCategory>();
                store.DefineTable<Country>();
                store.DefineTable<Currency>();
                store.DefineTable<Models.DataObjects.PartnerGrade>();
                store.DefineTable<PartnerTitle>();
                store.DefineTable<User>();
                store.DefineTable<Product>();
                store.DefineTable<ProductPriceListItem>();
                store.DefineTable<ProductPriceList>();
                store.DefineTable<ProductUOM>();
                store.DefineTable<PurchaseOrderLine>();
                store.DefineTable<PurchaseOrder>();
                store.DefineTable<SaleOrder>();
                store.DefineTable<SaleOrderLine>();
                store.DefineTable<Event>();
                store.DefineTable<EventAlarm>();
                store.DefineTable<Message>();

                store.DefineTable<StoreSettings>();

                //TODO Add rest of the tables
            }

            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler()).ConfigureAwait(false);

            await LoadCachedTokenAsync().ConfigureAwait(false);
        }


        public async Task<bool> SyncAllAsync(bool syncUserSpecific)
        {
            if (!IsInitialized)
                await InitializeAsync();

            var taskList = new List<Task<bool>>();

            taskList.Add(CustomerStore.SyncAsync());
            taskList.Add(PartnerCategoryStore.SyncAsync());

            taskList.Add(CurrencyStore.SyncAsync());
            taskList.Add(CountryStore.SyncAsync());
            taskList.Add(PartnerGradeStore.SyncAsync());
            taskList.Add(PartnerTitleStore.SyncAsync());
            taskList.Add(UserStore.SyncAsync());
            taskList.Add(ProductStore.SyncAsync());
            taskList.Add(ProductPriceListItemStore.SyncAsync());
            taskList.Add(ProductPriceListStore.SyncAsync());
            taskList.Add(ProductUOMStore.SyncAsync());
            taskList.Add(PurchaseOrderLineStore.SyncAsync());
            taskList.Add(PurchaseOrderStore.SyncAsync());
            taskList.Add(SaleOrderStore.SyncAsync());
            taskList.Add(SaleOrderLineStore.SyncAsync());
            taskList.Add(EventStore.SyncAsync());
            taskList.Add(EventAlarmStore.SyncAsync());
            taskList.Add(MessageStore.SyncAsync());


            //TODO add all other stores

            if (syncUserSpecific)
            {
                // add stores that are user specific data
            }

            var successes = await Task.WhenAll(taskList).ConfigureAwait(false);
            return successes.Any(x => !x); //if any were a failure.

        }

        #endregion



        public async Task<MobileServiceUser> LoginAsync(string username, string password)
        {
            if (!IsInitialized)
            {
                await InitializeAsync();
            }

            var credentials = new JObject();
            credentials["username"] = username;
            credentials["password"] = password;

            var uri = new Uri("http://voltairecrm.azurewebsites.net/api/login");

            try
            {
                var _client = new HttpClient();
                var json_cred = credentials.ToString();
                var content = new StringContent(json_cred, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var content2 = await response.Content.ReadAsStringAsync();

                    var User = JsonConvert.DeserializeObject<USER>(content2);

                    MobileServiceUser user = new MobileServiceUser(User.UserId.ToString()) { MobileServiceAuthenticationToken = User.Token };

                    MobileService.CurrentUser = user;

                    await CacheToken(user);

                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }


        public async Task<bool> LogoutAsync()
        {
            if (!IsInitialized)
            {
                await InitializeAsync();
            }

            await MobileService.LogoutAsync();

            var settings = await ReadSettingsAsync();

            if (settings != null)
            {
                settings.AuthToken = string.Empty;
                settings.UserId = string.Empty;

                var result = await SaveSettingsAsync(settings);
                return result;
            }
            else
                return true;

        }


        public async Task<bool> SaveSettingsAsync(StoreSettings settings)
        {
            try
            {
                await MobileService.SyncContext.Store.UpsertAsync(nameof(StoreSettings), new[] { JObject.FromObject(settings) }, true);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

            return true;
        }


        public async Task<StoreSettings> ReadSettingsAsync()
        {
            try
            {
                var response = (await MobileService.SyncContext.Store.LookupAsync(nameof(StoreSettings), StoreSettings.StoreSettingsId))?.ToObject<StoreSettings>();
                return response;
            }
            catch (Exception)
            {
                return null;
            }
        }



        async Task CacheToken(MobileServiceUser user)
        {
            var settings = new StoreSettings
            {
                UserId = user.UserId,
                AuthToken = user.MobileServiceAuthenticationToken
            };

            await SaveSettingsAsync(settings);
        }


        async Task LoadCachedTokenAsync()
        {
            StoreSettings settings = await ReadSettingsAsync();

            if (settings != null)
            {
                try
                {
                    if (!string.IsNullOrEmpty(settings.AuthToken) && JwtUtility.GetTokenExpiration(settings.AuthToken) > DateTime.UtcNow)
                    {
                        MobileService.CurrentUser = new MobileServiceUser(settings.UserId);
                        MobileService.CurrentUser.MobileServiceAuthenticationToken = settings.AuthToken;
                    }
                }
                catch (InvalidTokenException)
                {
                    settings.AuthToken = string.Empty;
                    settings.UserId = string.Empty;

                    await SaveSettingsAsync(settings);
                }
            }
        }


        public class StoreSettings
        {
            public const string StoreSettingsId = "store_settings";

            public StoreSettings()
            {
                Id = StoreSettingsId;
            }

            public string Id { get; set; }

            public string UserId { get; set; }

            public string AuthToken { get; set; }
        }


        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }


        public class USER
        {
            [JsonProperty("token")]
            public string Token { get; set; }

            [JsonProperty("userId")]
            public long UserId { get; set; }
        }

    }
}
