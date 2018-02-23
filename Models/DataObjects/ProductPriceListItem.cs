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
        public DateTime DateStart { get; set; }

        [JsonProperty("date_end")]
        public DateTime DateEnd { get; set; }

        [JsonProperty("fixed_price")]
        public long FixedPrice { get; set; }

        [JsonProperty("compute_price")]
        public long ComputePrice { get; set; }

        [JsonProperty("min_quantity")]
        public long MinQuantity { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("write_date")]
        public DateTime WriteDate { get; set; }

        [JsonProperty("external_id")]
        public long ExternalId { get; set; }

        [JsonProperty("pricelist_id")]
        public long PricelistId { get; set; }

        [JsonProperty("product_tmpl_id")]
        public long ProductTmplId { get; set; }

        [JsonProperty("categ_id")]
        public long CategId { get; set; }

        [JsonProperty("applied_on")]
        public string AppliedOn { get; set; }

        [JsonProperty("sequence")]
        public long Sequence { get; set; }
    }
}
