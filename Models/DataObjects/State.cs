using System;
using Newtonsoft.Json;
using voltaire.DataStore.Abstraction;

namespace voltaire.Models.DataObjects
{
    public class State : BaseDataObject
    {
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

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("country_id")]
        public long CountryId { get; set; }
    }
}
