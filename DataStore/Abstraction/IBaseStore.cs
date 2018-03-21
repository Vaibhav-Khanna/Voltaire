using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace voltaire.DataStore.Abstraction
{
    
    public interface IBaseStore<T>
    {

        Task InitializeStore();
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false,bool AllItems = false);
        Task<IEnumerable<T>> GetNextItemsAsync(int currentitemCount);
        Task<T> GetItemAsync(string id);
        Task<bool> InsertAsync(T item);
        Task<bool> UpdateAsync(T item);
        Task<bool> RemoveAsync(T item);
        Task<bool> SyncAsync();

        Task DropTable();

        string Identifier { get; }

    }

}