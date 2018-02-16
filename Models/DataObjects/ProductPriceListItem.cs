namespace voltaire.Models.DataObjects
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using voltaire.DataStore.Abstraction;

    public class ProductPriceListItem : BaseDataObject
    {
       
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("date_start")]
        public System.DateTime DateStart { get; set; }

        [JsonProperty("date_end")]
        public System.DateTime DateEnd { get; set; }

        [JsonProperty("fixed_price")]
        public long FixedPrice { get; set; }

        [JsonProperty("min_quantity")]
        public long MinQuantity { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("percent_price")]
        public long PercentPrice { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("price_discount")]
        public long PriceDiscount { get; set; }

        [JsonProperty("price_round")]
        public long PriceRound { get; set; }

        [JsonProperty("price_surcharge")]
        public long PriceSurcharge { get; set; }

        [JsonProperty("write_date")]
        public System.DateTime WriteDate { get; set; }

        [JsonProperty("external_id")]
        public long ExternalId { get; set; }

        [JsonProperty("priceListId")]
        public long PriceListId { get; set; }
    }
}
