using System;
using System.Threading.Tasks;
using voltaire.Models.DataObjects;

namespace voltaire.DataStore.Abstraction.Stores
{
    public interface IDocumentStore : IBaseStore<Document>
    {
        Task<Document> GetItemByEmployeeId(string ID, string internalName);

        Task<byte[]> GetDocumentDataById(string key);

        Task<bool> InsertImage(byte[] data, Document document);

        Task<bool> OfflineUploadSync();
    }
}
