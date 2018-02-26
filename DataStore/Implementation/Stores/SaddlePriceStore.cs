using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;
namespace voltaire.DataStore.Implementation.Stores
{
    public class SaddlePriceStore: BaseStore<Saddle>, ISaddlePriceStore
    {
        public override string Identifier => "Saddle";
    }
}
