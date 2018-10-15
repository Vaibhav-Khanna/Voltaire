using System;
using Newtonsoft.Json;

namespace voltaire.Models.DataObjects
{
    public class DeliveryFee
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("brand_id")]
        public long BrandId { get; set; }

        [JsonProperty("product_id")]
        public long ProductId { get; set; }

        [JsonProperty("price")]
        public long Price { get; set; }

        [JsonProperty("currency_id")]
        public long CurrencyId { get; set; }

        [JsonIgnore]
        public string PriceDisplay { get { return $"{Type} - {Price} {ProductConstants.CurrencyValues[CurrencyId]}" ; } }
    }
}
