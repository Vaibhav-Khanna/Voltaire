using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using voltaire.DataStore.Abstraction;

namespace voltaire.Models.DataObjects
{
    public class Product : BaseDataObject
    {

       
        [JsonIgnore]
        public List<ProductProperty> Properties { get; set; } = new List<ProductProperty>();

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("external_id")]
        public long ExternalId { get; set; }

        [JsonProperty("write_date")]
        public DateTime WriteDate { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("categ_id")]
        public int Categ_id { get; set; }
    }
}
