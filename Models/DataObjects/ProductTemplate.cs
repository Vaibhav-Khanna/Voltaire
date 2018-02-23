using System;
using Newtonsoft.Json;
using voltaire.DataStore.Abstraction;

namespace voltaire.Models.DataObjects
{
    public class ProductTemplate : BaseDataObject
    {
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("company_id")]
        public long CompanyId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("default_code")]
        public string DefaultCode { get; set; }

        [JsonProperty("write_date")]
        public DateTime WriteDate { get; set; }

        [JsonProperty("external_id")]
        public long ExternalId { get; set; }
    }
}
