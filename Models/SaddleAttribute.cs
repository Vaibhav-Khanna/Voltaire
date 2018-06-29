using System;
using Newtonsoft.Json;

namespace voltaire.Models
{
    public class SaddleAttribute
    {
        [JsonProperty("de_DE")]
        public string De { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("fr_FR")]
        public string Fr { get; set; }

        [JsonProperty("en_US")]
        public string En { get; set; }
    }
}
