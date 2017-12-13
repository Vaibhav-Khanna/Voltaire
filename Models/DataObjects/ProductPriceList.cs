namespace voltaire.Models.DataObjects
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using voltaire.DataStore.Abstraction;

    public class ProductPriceList : BaseDataObject
    {
       
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("externalId")]
        public long ExternalId { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("company_id")]
        public long CompanyId { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("write_date")]
        public DateTime WriteDate { get; set; }
    }
}
