namespace voltaire.Models.DataObjects
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using voltaire.DataStore.Abstraction;

    public class SaddlePrice : BaseDataObject
    {
      
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("leather")]
        public string Leather { get; set; }

        [JsonProperty("sk2")]
        public bool Sk2 { get; set; }

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

        [JsonProperty("nameplace_price_dollard")]
        public long NameplacePriceDollard { get; set; }

        [JsonProperty("nameplace_price_euro")]
        public long NameplacePriceEuro { get; set; }

        [JsonProperty("nameplace_price_gpb")]
        public long NameplacePriceGpb { get; set; }

        [JsonProperty("nameplace_price_sek")]
        public long NameplacePriceSek { get; set; }

        [JsonProperty("nameplace_price_dkk")]
        public long NameplacePriceDkk { get; set; }

        [JsonProperty("nameplace_price_chf")]
        public long NameplacePriceChf { get; set; }

        [JsonProperty("greasing_price_dollard")]
        public long GreasingPriceDollard { get; set; }

        [JsonProperty("greasing_price_euro")]
        public long GreasingPriceEuro { get; set; }

        [JsonProperty("greasing_price_gpb")]
        public long GreasingPriceGpb { get; set; }

        [JsonProperty("greasing_price_sek")]
        public long GreasingPriceSek { get; set; }

        [JsonProperty("greasing_price_dkk")]
        public long GreasingPriceDkk { get; set; }

        [JsonProperty("greasing_price_chf")]
        public long GreasingPriceChf { get; set; }

        [JsonProperty("price_gbp")]
        public long PriceGbp { get; set; }

        [JsonProperty("nameplate_price_dollard")]
        public long NameplatePriceDollard { get; set; }

        [JsonProperty("nameplate_price_euro")]
        public long NameplatePriceEuro { get; set; }

        [JsonProperty("nameplate_price_gbp")]
        public long NameplatePriceGbp { get; set; }

        [JsonProperty("nameplate_price_sek")]
        public long NameplatePriceSek { get; set; }

        [JsonProperty("nameplate_price_dkk")]
        public long NameplatePriceDkk { get; set; }

        [JsonProperty("nameplate_price_chf")]
        public long NameplatePriceChf { get; set; }

        [JsonProperty("greasing_price_gbp")]
        public long GreasingPriceGbp { get; set; }
    }
}
