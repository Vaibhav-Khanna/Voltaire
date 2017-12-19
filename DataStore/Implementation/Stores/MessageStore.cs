using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models;

namespace voltaire.DataStore.Implementation.Stores
{
    public class MessageStore : BaseStore<Message>, IMessageStore
    {
        public override string Identifier => "Message";


        public virtual async Task<IEnumerable<Message>> GetMessagesBySaleOrderIdAsync(string saleOrderId)
        {
            await InitializeStore().ConfigureAwait(false);
            await PullLatestAsync().ConfigureAwait(false);
            var items = await Table.Where(s => s.ResId == saleOrderId).ToListAsync().ConfigureAwait(false);

            if (items == null || items.Count == 0)
                return null;

            return items;
        }


    }
}
