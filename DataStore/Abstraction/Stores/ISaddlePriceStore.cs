using System;
using voltaire.Models.DataObjects;
using System.Threading.Tasks;
using System.Collections.Generic;
using voltaire.Models;

namespace voltaire.DataStore.Abstraction.Stores
{
    public interface ISaddlePriceStore : IBaseStore<Saddle>
    {
        Task<IEnumerable<SaddleAttribute>> GetSaddleAttributes(); 

        Task<IEnumerable<SaddleModel>> GetSaddleModel();  

        Task<IEnumerable<SaddleValue>> GetSaddleValue();  
    }  
}
