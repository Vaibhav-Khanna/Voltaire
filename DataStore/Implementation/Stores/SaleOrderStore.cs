using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models;
using Newtonsoft.Json;
using voltaire.Models.DataObjects;
using voltaire.Helpers;
using Plugin.Connectivity;

namespace voltaire.DataStore.Implementation.Stores
{
    public class SaleOrderStore : BaseStore<SaleOrder>, ISaleOrderStore
    {
        public override string Identifier => "SaleOrder";


        public async Task<IEnumerable<SaleOrder>> GetOrderItemsByCustomer(long PartnerId)
        {
            await InitializeStore().ConfigureAwait(false);

            return await Table.Where( x => x.PartnerId == PartnerId ).Where( x => x.State == QuotationStatus.sale.ToString() || x.State == QuotationStatus.done.ToString() ).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<SaleOrder>> GetQuotationItemsByCustomer(long PartnerId)
        {
            await InitializeStore().ConfigureAwait(false);

            return await Table.Where(x => x.PartnerId == PartnerId).Where(x => x.State == QuotationStatus.draft.ToString() || x.State == QuotationStatus.sent.ToString()).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<SaleOrder>> GetQuotations(int currentCount)
        {
            await InitializeStore().ConfigureAwait(false);

            return await Table.Where(x => x.State == QuotationStatus.draft.ToString() || x.State == QuotationStatus.sent.ToString()).OrderByDescending(x => x.CreateDate).Skip(currentCount).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<SaleOrder>> GetOrders(int currentCount)
        {
            await InitializeStore().ConfigureAwait(false);

            return await Table.Where(x => x.State == QuotationStatus.sale.ToString() || x.State == QuotationStatus.done.ToString()).OrderByDescending(x => x.CreateDate).Skip(currentCount).Take(50).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);
        }


        public async Task<IEnumerable<DeliveryFee>> GetDeliveryFees(bool forceRefresh)
        {
            if (forceRefresh)
            {
                if (await CrossConnectivity.Current.IsRemoteReachable("https://www.google.com"))
                {
                    var uri = new Uri(Constants.EndUrl + "/api/deliveryFee");

                    try
                    {
                        var _client = new HttpClient();

                        _client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");
                        _client.DefaultRequestHeaders.Add("token", StoreManager.MobileService.CurrentUser.MobileServiceAuthenticationToken);

                        var response = await _client.GetAsync(uri);

                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();

                            if (!string.IsNullOrWhiteSpace(content))
                            {
                                var result = JsonConvert.DeserializeObject<IEnumerable<DeliveryFee>>(content);

                                Settings.DeliveryFee = content;

                                return result;
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(Settings.DeliveryFee))
                    {
                        var result = JsonConvert.DeserializeObject<IEnumerable<DeliveryFee>>(Settings.DeliveryFee);

                        return result;
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Settings.DeliveryFee))
                {
                    var result = JsonConvert.DeserializeObject<IEnumerable<DeliveryFee>>(Settings.DeliveryFee);

                    return result;
                }
            }

            return null;
        }

    }
}
