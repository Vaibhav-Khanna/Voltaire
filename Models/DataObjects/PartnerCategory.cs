using System;
using Newtonsoft.Json;
using voltaire.DataStore.Abstraction;

namespace voltaire.Models.DataObjects
{
    public class PartnerCategory : BaseDataObject
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("color")]
        public int Color { get; set; }

        [JsonProperty("display_name")]
        public string Display_name { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("parent_right")]
        public int Parent_right { get; set; }

        [JsonProperty("write_date")]
        public DateTimeOffset WriteDate { get; set; }


    }
}
