using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;
using System.Net.Http;
using Newtonsoft.Json;
using voltaire.Models;

using System.Linq;
using System.Reactive.Linq;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Newtonsoft.Json.Linq;

namespace voltaire.DataStore.Implementation.Stores
{
    public class SaddlePriceStore: BaseStore<Saddle>, ISaddlePriceStore
    {
        public override string Identifier => "Saddle";

        IConnectivity Connectivity => CrossConnectivity.Current;

        public async Task<IEnumerable<SaddleAttribute>> GetSaddleAttributes()
        {
            try
            {
                if (await Connectivity.IsRemoteReachable("https://www.google.com"))
                {
                    var url = new Uri(Constants.EndUrl + "/api/saddleAttribute");

                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Add("token", StoreManager.MobileService.CurrentUser.MobileServiceAuthenticationToken);

                    var response = await client.GetAsync(url);
                    var data = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var list = JsonConvert.DeserializeObject<IEnumerable<SaddleAttribute>>(data);

                        if (list != null && list.Any())
                        {
                            await StoreManager.MobileService.SyncContext.Store.UpsertAsync(nameof(SaddleAttribute), list.Select((arg) => JObject.FromObject(arg)),true);
                        }

                        return list;
                    }
                }
                else
                {
                    var objects = await StoreManager.MobileService.SyncContext.Store.ReadAsync(new Microsoft.WindowsAzure.MobileServices.Query.MobileServiceTableQueryDescription(nameof(SaddleAttribute)){ IncludeTotalCount = true });

                    var str = objects.Value<JArray>("results");

                    var items = str.Select((arg) => arg.ToObject<SaddleAttribute>());                 

                    return items;
                }
            }
            catch(Exception ex)
            { 
                
            }

            return null;
        }

        public async Task<IEnumerable<SaddleModel>> GetSaddleModel()
        {
           

            try
            {
                if (await Connectivity.IsRemoteReachable("https://www.google.com"))
                {
                    var url = new Uri(Constants.EndUrl + "/api/saddleModel");

                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Add("token", StoreManager.MobileService.CurrentUser.MobileServiceAuthenticationToken);

                    var response = await client.GetAsync(url);
                    var data = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var list = JsonConvert.DeserializeObject<IEnumerable<SaddleModel>>(data);

                        if (list != null && list.Any())
                        {
                            int i = 0;

                            foreach (var item in list)
                            {
                                item.Id = i;
                                i++;
                            }

                            await StoreManager.MobileService.SyncContext.Store.UpsertAsync(nameof(SaddleModel), list.Select((arg) => JObject.FromObject(arg)), true);
                        }

                        return list;
                    }
                }
                else
                {
                    var objects = await StoreManager.MobileService.SyncContext.Store.ReadAsync(new Microsoft.WindowsAzure.MobileServices.Query.MobileServiceTableQueryDescription(nameof(SaddleModel)) { IncludeTotalCount = true });

                    var str = objects.Value<JArray>("results");

                    var items = str.Select((arg) => arg.ToObject<SaddleModel>());

                    return items;
                }
            }
            catch (Exception)
            {
            }

            return null;
        }

        public async Task<IEnumerable<SaddleValue>> GetSaddleValue()
        {         
            try
            {
                if (await Connectivity.IsRemoteReachable("https://www.google.com"))
                {
                    var url = new Uri(Constants.EndUrl + "/api/saddleValue");

                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Add("token", StoreManager.MobileService.CurrentUser.MobileServiceAuthenticationToken);

                    var response = await client.GetAsync(url);
                    var data = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var list = JsonConvert.DeserializeObject<IEnumerable<SaddleValue>>(data);

                        if (list != null && list.Any())
                        {
                            await StoreManager.MobileService.SyncContext.Store.UpsertAsync(nameof(SaddleValue), list.Select((arg) => JObject.FromObject(arg)), true);
                        }

                        return list;
                    }
                }
                else
                {
                    var objects = await StoreManager.MobileService.SyncContext.Store.ReadAsync(new Microsoft.WindowsAzure.MobileServices.Query.MobileServiceTableQueryDescription(nameof(SaddleValue)) { IncludeTotalCount = true });

                    var str = objects.Value<JArray>("results");

                    var items = str.Select((arg) => arg.ToObject<SaddleValue>());

                    return items;
                }
            }
            catch (Exception)
            {
            }

            return null;
        }
    }
}
