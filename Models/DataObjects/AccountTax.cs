using System;
using Newtonsoft.Json;
using voltaire.DataStore.Abstraction;

namespace voltaire.Models.DataObjects
{
    public class AccountTax : BaseDataObject
    {
    
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("external_id")]
        public long ExternalId { get; set; }

        [JsonProperty("type_tax_use")]
        public string TypeTaxUse { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("write_date")]
        public DateTime WriteDate { get; set; }
    
    }

    public enum TaxUse 
    {
        sale, purchase, none
    }

}
