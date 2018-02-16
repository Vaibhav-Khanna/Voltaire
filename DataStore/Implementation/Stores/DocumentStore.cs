using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Plugin.Connectivity;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Helpers;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Implementation.Stores
{
    public class DocumentStore : BaseStore<Document>, IDocumentStore
    {
        public override string Identifier => "Document";

        public async Task<byte[]> GetDocumentDataById(string key)
        {
            var data = await PclStorage.LoadFileLocal(key);
            return data;
        }

        public async Task<bool> InsertImage(byte[] data, Document document)
        {
            if (data == null)
                return false;
            
            document.ToUpload = true;

            var response = await PclStorage.SaveFileLocal(data, document.Id);

            var resp = await InsertAsync(document);

            var res = await UploadDocument(data, document);
           
            if (res)
            {
                document.ToUpload = false;
                await UpdateAsync(document);
            }

            return res;
        }

        public async Task<Document> GetItemByEmployeeId(string ID, string internalName)
        {
            await InitializeStore().ConfigureAwait(false);

            var items = await Table.Where(x => x.ReferenceId == ID && x.InternalName == internalName ).IncludeTotalCount().ToEnumerableAsync().ConfigureAwait(false);

            if (items != null && items.Any())
                return items.FirstOrDefault();
            else
                return null;
        }

        public virtual async Task<bool> OfflineUploadSync()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                await InitializeStore().ConfigureAwait(false);

                var items = await GetItemsAsync(true);

                foreach (var item in items)
                {
                    if (item.ToUpload)
                    {
                        var file = await PclStorage.LoadFileLocal(item.Id);

                        if (file != null)
                        {
                            var isUploaded = await UploadDocument(file, item);

                            if (isUploaded)
                            {
                                item.ToUpload = false;
                                await UpdateAsync(item);
                            }
                        }
                    }
                }

                await PullLatest();

                return true;
            }
            else
                return false;
        }

        private async Task PullLatest()
        {
            await InitializeStore().ConfigureAwait(false);
                      
            var items = await GetItemsAsync(true);

            foreach (var item in items)
            {
                var IsExist = await PclStorage.IsFileExistAsync(item.Id);

                if (!IsExist)
                {
                    var document = await DownLoadDocument(item.Id);
                   
                    if (document != null)
                    {
                        var response = await PclStorage.SaveFileLocal(document, item.Id);
                    }
                }
            }
        }

        private async Task<bool> UploadDocument(byte[] data, Document document)
        {
            try
            {
                HttpClient client = new HttpClient();
                MultipartFormDataContent content = new MultipartFormDataContent();
                ByteArrayContent baContent = new ByteArrayContent(data);

                client.DefaultRequestHeaders.Add("X-ZUMO-AUTH", StoreManager.MobileService.CurrentUser.MobileServiceAuthenticationToken );
                client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

                content.Add(baContent, "uploadedFile", document.Name + document.Path.Substring(document.Path.LastIndexOf('.')));
                content.Add(new StringContent(document.ReferenceId), "referenceId");
                content.Add(new StringContent(document.ReferenceKind), "referenceKind");
                content.Add(new StringContent(document.InternalName), "internalName");
                content.Add(new StringContent(document.Name), "name");


                var response = await client.PostAsync(Constants.EndUrl + "/api/document/upload",content);

                if (response.IsSuccessStatusCode)
                    return true;
                else
                {
                    var resp = await response.Content.ReadAsStringAsync();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        private async Task<Byte[]> DownLoadDocument(string id)
        {
            var uri = new Uri( $"{Constants.EndUrl}/api/document/{id}/file?token={StoreManager.MobileService.CurrentUser.MobileServiceAuthenticationToken}");

            try
            {
                var _client = new HttpClient();

                var response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();

                    if (stream != null)
                        return StoreManager.ReadFully(stream);
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
                Debug.WriteLine(@"              ERROR {0}", ex.Message);
            }
        }
       

    }
}
