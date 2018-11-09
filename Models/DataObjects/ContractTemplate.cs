using System;
using Newtonsoft.Json;

namespace voltaire.Models.DataObjects
{
    public class ContractTemplate
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("pdf")]
        public string Pdf { get; set; }
    }
}
