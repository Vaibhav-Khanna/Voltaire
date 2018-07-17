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
using voltaire.PopUps;
using Plugin.Connectivity;
using System.Reactive.Linq;

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
       
        ISaleOrderStore saleOrderStore;
        public ISaleOrderStore SaleOrderStore => saleOrderStore ?? (saleOrderStore = DependencyService.Get<ISaleOrderStore>());

        ISaleOrderLineStore saleOrderLineStore;
        public ISaleOrderLineStore SaleOrderLineStore => saleOrderLineStore ?? (saleOrderLineStore = DependencyService.Get<ISaleOrderLineStore>());

        IMessageStore messageStore;
        public IMessageStore MessageStore => messageStore ?? (messageStore = DependencyService.Get<IMessageStore>());

        ICheckinStore checkinStore;
        public ICheckinStore CheckinStore => checkinStore ?? (checkinStore = DependencyService.Get<ICheckinStore>());

        IDocumentStore documentStore;
        public IDocumentStore DocumentStore => documentStore ?? ( documentStore = DependencyService.Get<IDocumentStore>());

        ICompanyStore companyStore;
        public ICompanyStore CompanyStore => companyStore ?? (companyStore = DependencyService.Get<ICompanyStore>());

        IAccountTaxStore accountTaxStore;
        public IAccountTaxStore AccountTaxStore => accountTaxStore ?? (accountTaxStore = DependencyService.Get<IAccountTaxStore>());

        IAccessoryStore accessoryStore;
        public IAccessoryStore AccessoryStore => accessoryStore ?? (accessoryStore = DependencyService.Get<IAccessoryStore>());

        ISaddlePriceStore saddleStore;
        public ISaddlePriceStore SaddleStore => saddleStore ?? (saddleStore = DependencyService.Get<ISaddlePriceStore>());

        IServiceStore serviceStore;
        public IServiceStore ServiceStore => serviceStore ?? (serviceStore = DependencyService.Get<IServiceStore>());

        IStateStore stateStore;
        public IStateStore StateStore => stateStore ?? (stateStore = DependencyService.Get<IStateStore>());


        #region iStoreManager Implementation

        public async Task DropEverythingAsync()
        {
            //Settings.UpdateDatabaseId();
            //TODO Do the update id for settings and add rest of the tables

            await CustomerStore.DropTable();
            await PartnerCategoryStore.DropTable();
            await CountryStore.DropTable();
            await CurrencyStore.DropTable();
            await PartnerGradeStore.DropTable();
            await PartnerTitleStore.DropTable();
            await UserStore.DropTable();

            await SaleOrderStore.DropTable();
            await SaleOrderLineStore.DropTable();

            await MessageStore.DropTable();
            await CheckinStore.DropTable();
            await DocumentStore.DropTable();
            await CompanyStore.DropTable();
            await AccountTaxStore.DropTable();

            await AccessoryStore.DropTable();
            await ServiceStore.DropTable();
            await SaddleStore.DropTable();
            await StateStore.DropTable();

            IsInitialized = false;

            Settings.UpdateDatabaseId();
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


                var dbId = Settings.DatabaseId;

                string path = "";

                if (dbId == 0)
                    path = $"syncstore.db";
                else
                    path = $"syncstore{dbId}.db";

                MobileService = new MobileServiceClient(Constants.EndUrl);
                store = new MobileServiceSQLiteStore(path);

                store.DefineTable<Partner>();
                store.DefineTable<PartnerCategory>();
                store.DefineTable<Country>();
                store.DefineTable<Currency>();
                store.DefineTable<Models.DataObjects.PartnerGrade>();
                store.DefineTable<PartnerTitle>();
                store.DefineTable<User>();
                           
                store.DefineTable<SaleOrder>();
                store.DefineTable<SaleOrderLine>();
                store.DefineTable<Message>();
                store.DefineTable<Checkin>();
                store.DefineTable<Document>();
                store.DefineTable<Company>();
                store.DefineTable<AccountTax>();
                store.DefineTable<Accessory>();
                store.DefineTable<Saddle>();
                store.DefineTable<Service>();
                store.DefineTable<Models.DataObjects.State>();

                store.DefineTable<StoreSettings>();

                store.DefineTable<SaddleAttribute>();
                store.DefineTable<SaddleModel>();
                store.DefineTable<SaddleValue>();

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

            taskList.Add(UserStore.SyncAsync());
            taskList.Add(CustomerStore.SyncAsync());
            taskList.Add(PartnerCategoryStore.SyncAsync());
            taskList.Add(CheckinStore.SyncAsync());
            taskList.Add(PartnerGradeStore.SyncAsync());
            taskList.Add(PartnerTitleStore.SyncAsync());
            taskList.Add(CurrencyStore.SyncAsync());
            taskList.Add(CountryStore.SyncAsync());          
            taskList.Add(SaleOrderStore.SyncAsync());
            taskList.Add(SaleOrderLineStore.SyncAsync());
            taskList.Add(MessageStore.SyncAsync());
            taskList.Add(DocumentStore.SyncAsync());
            taskList.Add(CompanyStore.SyncAsync());
            taskList.Add(AccountTaxStore.SyncAsync());
            taskList.Add(SaddleStore.SyncAsync());
            taskList.Add(ServiceStore.SyncAsync());
            taskList.Add(AccessoryStore.SyncAsync());
            taskList.Add(StateStore.SyncAsync());


            Device.BeginInvokeOnMainThread(async () =>
           {
               await ToastService.Show("The app is currently syncing... This might take a few minutes");
           });


            //TODO add all other stores
                       
            var successes = await Task.WhenAll(taskList).ConfigureAwait(false);

            if (syncUserSpecific)
            {
                // add stores that are user specific data
                await DocumentStore.OfflineUploadSync();

                await SyncLegalFiles();
            }

            Device.BeginInvokeOnMainThread(async () =>
            {
                await ToastService.Hide();
            });

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
            credentials["email"] = username;
            credentials["password"] = password;
            credentials["mobile"] = true;

            var uri = new Uri(Constants.EndUrl + "/api/login");

            try
            {
                var _client = new HttpClient();

                _client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");
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

        public async Task VerifyTokenAsync()
        {

            StoreSettings settings = await ReadSettingsAsync();

            if (settings != null)
            {
                try
                {
                    if (!string.IsNullOrEmpty(settings.AuthToken) && JwtUtility.GetTokenExpiration(settings.AuthToken) < DateTime.UtcNow)
                    {
                        if (CrossConnectivity.Current.IsConnected)
                        {
                            var result = await RegenerateTokenAsync();

                            if (!result)
                            {
                                //no token regenerated
                                await LogoutAsync();
                            }
                        }
                    }
                }
                catch (InvalidTokenException)
                {
                    //Token exception error
                    if (CrossConnectivity.Current.IsConnected)
                    {
                        await LogoutAsync();
                    }
                }
            }

        }

        private async Task<bool> RegenerateTokenAsync()
        {
            var uri = new Uri(Constants.EndUrl + "/api/regenerate");

            StoreSettings settings = await ReadSettingsAsync();

            var actualToken = settings.AuthToken;

            try
            {
                var _client = new HttpClient();
                _client.DefaultRequestHeaders.Add("token", actualToken);
                _client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

                var response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var User = JsonConvert.DeserializeObject<USER>(content);
                    MobileServiceUser user = new MobileServiceUser(User.UserId.ToString()) { MobileServiceAuthenticationToken = User.Token };
                    MobileService.CurrentUser = user;

                    await CacheToken(user);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
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

        public async Task<bool> SyncLegalFiles()
        {
            var language = "en";

            var uri = new Uri($"{Constants.EndUrl}/api/legalFiles?language={language}");

            try
            {
                var actualToken = StoreManager.MobileService.CurrentUser.MobileServiceAuthenticationToken;

                var _client = new HttpClient();
                _client.DefaultRequestHeaders.Add("token", actualToken);

                var response = await _client.GetAsync(uri);
                var data_string = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var data = JsonConvert.DeserializeObject<LegalFilesModel>(data_string);

                    // download terms and conditions
                    if (!string.IsNullOrEmpty(data.TermAndConditions))
                    {
                        _client = new HttpClient();

                        response = await _client.GetAsync(data.TermAndConditions);

                        var file = await response.Content.ReadAsByteArrayAsync();

                        if (response.IsSuccessStatusCode)
                        {
                            if (file != null)
                            {
                                var isSaved = await Helpers.PclStorage.SaveFileLocal(file, StorageKeys.TermsConditions);
                                return isSaved;
                            }
                        }
                    }
                } 
            }
            catch (Exception)
            {
                
            }

            return false;
        }

        public async Task<Dictionary<string, byte[]>> GetLegalFiles()
        {
            var list = new Dictionary<string, byte[]>();

            // Terms conditions
            try
            {
                var file = await Helpers.PclStorage.LoadFileLocal(StorageKeys.TermsConditions);

                if(file!=null)
                {
                    list.Add(StorageKeys.TermsConditions,file);
                }
            }
            catch (Exception)
            {
                
            }

            return list;
        }

        public class USER
        {
            [JsonProperty("userId")]
            public string UserId { get; set; }

            [JsonProperty("companyId")]
            public string CompanyId { get; set; }

            [JsonProperty("token")]
            public string Token { get; set; }

            [JsonProperty("tokenExpiration")]
            public long TokenExpiration { get; set; }
        }

    }
}
