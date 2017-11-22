﻿using System;
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

namespace voltaire.DataStore.Implementation
{
    public class StoreManager : IStoreManager
    {

        public static MobileServiceClient MobileService { get; set; }

        public bool IsInitialized { get; private set; }


        ICustomerStore customerStore;
        public ICustomerStore CustomerStore => customerStore ?? (customerStore = DependencyService.Get<ICustomerStore>());

        IQuotationStore quotationStore;
        public IQuotationStore QuotationStore => quotationStore ?? (quotationStore = DependencyService.Get<IQuotationStore>());

        IContractStore contractStore;
        public IContractStore ContractStore => contractStore ?? (contractStore = DependencyService.Get<IContractStore>());


        #region iStoreManager Implementation

        public Task DropEverythingAsync()
        {
            //Settings.UpdateDatabaseId();
            //TODO Do the update id for settings and add rest of the tables

            CustomerStore.DropTable();
            QuotationStore.DropTable();
            ContractStore.DropTable();

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
                store.DefineTable<Contract>();
                store.DefineTable<Quotation>();
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
            taskList.Add(QuotationStore.SyncAsync());
            taskList.Add(ContractStore.SyncAsync());

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

                    MobileServiceUser user = new MobileServiceUser(User.UserId.ToString()){ MobileServiceAuthenticationToken = User.Token };
          
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
            catch(Exception ex)
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
            catch(Exception)
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
