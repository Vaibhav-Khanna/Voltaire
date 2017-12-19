using System;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.Models.DataObjects;
namespace voltaire.DataStore.Implementation.Stores
{
    public class SaddlePriceStore: BaseStore<SaddlePrice>, ISaddlePriceStore
    {
        public override string Identifier => "SaddlePrice";
    }
}
