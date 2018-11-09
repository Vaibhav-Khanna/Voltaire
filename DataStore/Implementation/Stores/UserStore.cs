using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FreshMvvm;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;
using voltaire.PageModels;
using System.Linq;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Text;
using Newtonsoft.Json;
using voltaire.Models;

namespace voltaire.DataStore.Implementation.Stores
{
    public class UserStore : BaseStore<User>, IUserStore
    {
        public override string Identifier => "User";

        public User currentUser;

        public async Task<User> GetCurrentUserAsync()
        {
            
            if (currentUser != null)
                return currentUser;

            if (StoreManager.MobileService.CurrentUser == null)
            {
                return null;              
            }

            var id = StoreManager.MobileService.CurrentUser.UserId;

            if (id != null)
            {
                try
                {
                    await InitializeStore().ConfigureAwait(false);
                   
                    var items = await Table.Where(s => s.ExternalId.ToString() == id).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);

                    if (items == null || !items.Any())
                        return null;

                    currentUser = items.First();

                    return currentUser;

                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public virtual async Task<User> GetUserByExternalIdAsync(long externalId)
        {
            try
            {
                await InitializeStore().ConfigureAwait(false);
                await PullLatestAsync().ConfigureAwait(false);
                var items = await Table.Where(s => s.ExternalId == externalId).ToListAsync().ConfigureAwait(false);

                if (items == null || items.Count == 0)
                    return null;

                return items[0];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<UserSale>> GetSalesForMonth(int month, int year, bool IsSales)
        {
            var credentials = new JObject();
            credentials["month"] = month;
            credentials["year"] = year;
            credentials["typeOfSearch"] = IsSales ? "sales" : "checkins";

            var uri = new Uri(Constants.EndUrl + "/api/podium");

            try
            {
                var _client = new HttpClient();

                _client.DefaultRequestHeaders.Add("X-ZUMO-AUTH", StoreManager.MobileService.CurrentUser.MobileServiceAuthenticationToken);
                _client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");


                var json_cred = credentials.ToString();
                var content = new StringContent(json_cred, Encoding.UTF8, "application/json");


                var response = await _client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var content2 = await response.Content.ReadAsStringAsync();

                    var UserList = JsonConvert.DeserializeObject<List<UserSale>>(content2);

                    return UserList;
                }
               
            }
            catch (Exception)
            {

            }

            return null;
        }
    }
}
