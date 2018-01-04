namespace voltaire.Models.DataObjects
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using voltaire.DataStore.Abstraction;

    public class Service : BaseDataObject
    {
       
        //[JsonProperty("version")]
        //public string Version { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("price_dollard")]
        public long PriceDollard { get; set; }

        [JsonProperty("price_euro")]
        public long PriceEuro { get; set; }

        [JsonProperty("price_gpb")]
        public long PriceGpb { get; set; }

        [JsonProperty("price_sek")]
        public long PriceSek { get; set; }

        [JsonProperty("price_dkk")]
        public long PriceDkk { get; set; }

        [JsonProperty("price_chf")]
        public long PriceChf { get; set; }

        [JsonProperty("price_gbp")]
        public long PriceGbp { get; set; }
    }
}
