using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;
using System.Net.Http;
using Newtonsoft.Json;
using voltaire.Models;
using Akavache;
using System.Linq;
using System.Reactive.Linq;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;

namespace voltaire.DataStore.Implementation.Stores
{
    public class SaddlePriceStore: BaseStore<Saddle>, ISaddlePriceStore
    {
        public override string Identifier => "Saddle";

        private IBlobCache Storage => BlobCache.UserAccount;

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
                            var dictionary = new Dictionary<string, SaddleAttribute>();

                            foreach (var item in list)
                            {
                                dictionary.Add(item.Id.ToString(), item);
                            }

                            await Storage.InsertObjects<SaddleAttribute>(dictionary);
                        }

                        return list;
                    }
                }
                else
                {
                    var objects = await Storage.GetAllObjects<SaddleAttribute>();
                    return objects;
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
                            var dictionary = new Dictionary<string, SaddleModel>();

                            foreach (var item in list)
                            {
                                dictionary.Add(item.AttributeId.ToString(), item);
                            }

                            await Storage.InsertObjects<SaddleModel>(dictionary);
                        }

                        return list;
                    }
                }
                else
                {
                    var objects = await Storage.GetAllObjects<SaddleModel>();
                    return objects;
                }
            }
            catch (Exception ex)
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
                            var dictionary = new Dictionary<string, SaddleValue>();

                            foreach (var item in list)
                            {
                                dictionary.Add(item.Id.ToString(), item);
                            }

                            await Storage.InsertObjects<SaddleValue>(dictionary);
                        }

                        return list;
                    }
                }
                else
                {
                    var objects = await Storage.GetAllObjects<SaddleValue>();
                    return objects;
                }
            }
            catch (Exception ex)
            {

            }

            return null;
        }
    }
}
