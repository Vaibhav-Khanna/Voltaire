using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models;
using System.Net.Http;
using Newtonsoft.Json;
using voltaire.Helpers;
using System.Text;

namespace voltaire.DataStore.Implementation.Stores
{
	public class ContractStore : BaseStore<Contract>, IContractStore
    {
        public override string Identifier => "Contract";

        public async Task<IEnumerable<Contract>> GetContractsByPartnerExternalId(long id)
        {
            await InitializeStore().ConfigureAwait(false);

            try
            {             
                var items = await Table.Where(s => (s.PartnerId == id)).ToListAsync().ConfigureAwait(false);

                return items;
            }
            catch(Exception)
            {

            }

            return null;
        }

        public async Task<string> GetContractTemplate()
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("token", StoreManager.MobileService.CurrentUser.MobileServiceAuthenticationToken);
                client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

                var uri = new Uri(Constants.EndUrl + "/api/saleContractTemplate?language=" + Settings.DeviceLanguage);

                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    var data = JsonConvert.DeserializeObject<IEnumerable<PDFResponse>>(result);

                    if(!string.IsNullOrEmpty(data.First().PdfFile))
                    {
                        client = new HttpClient();

                        response = await client.GetAsync(data.First().PdfFile);

                        if (response.IsSuccessStatusCode)
                        {
                            var file = await response.Content.ReadAsByteArrayAsync();

                            if (file != null)
                            {
                                var isSaved = await Helpers.PclStorage.SaveFileLocal(file, StorageKeys.SaleContract);
                            }
                        }
                    }

                    return data.First().PdfFile;
                }
            }
            catch (Exception ex)
            {

            }

            return null;
        }


        public async Task<IEnumerable<string>> GetTermsConditionsOfContract()
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("token", StoreManager.MobileService.CurrentUser.MobileServiceAuthenticationToken);
                client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

                var uri = new Uri(Constants.EndUrl + "/api/contractTermsAndConditions");

                var response = await client.GetAsync(uri);

                if(response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    var data = JsonConvert.DeserializeObject<Response>(result);

                    return data.TermsAndConditions;
                }
            }
            catch(Exception)
            {

            }

            return null;
        }

        public class Response
        {
            [JsonProperty("termsAndConditions")]
            public List<string> TermsAndConditions { get; set; }
        }

        public class PDFResponse 
        {
            [JsonProperty("country")]
            public string Country { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("pdfFile")]
            public string PdfFile { get; set; }
        }

    }
}
