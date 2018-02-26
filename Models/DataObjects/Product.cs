using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using voltaire.DataStore.Abstraction;

namespace voltaire.Models.DataObjects
{
    public class Product : BaseDataObject
    {
           
        public List<ProductProperty> Properties { get; set; } = new List<ProductProperty>();

       
        public ProductKind ProductKind { get; set; }

      
        public long TaxId { get; set; }


        public long UnitPrice { get; set; }

     
        public string Name { get; set; }

      
        public string Description { get; set; }


        public int MinimumQuantity { get; set; }

    }
}
