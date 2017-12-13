using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models;

namespace voltaire.DataStore.Implementation.Stores
{
    public class MessageStore: BaseStore<Message>, IMessageStore
    {
        public override string Identifier => "Message";
    }
}
