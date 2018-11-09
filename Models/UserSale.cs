using System;
using Newtonsoft.Json;

namespace voltaire.Models
{
    public class UserSale
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonIgnore]
        public int Rank { get; set; }
    }
}
