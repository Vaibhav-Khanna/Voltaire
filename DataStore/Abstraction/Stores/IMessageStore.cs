using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using voltaire.Models;

namespace voltaire.DataStore.Abstraction.Stores
{
    public interface IMessageStore : IBaseStore<Message>
    {
        Task<IEnumerable<Message>> GetMessagesBySaleOrderIdAsync(string saleOrderId);
    }
}
