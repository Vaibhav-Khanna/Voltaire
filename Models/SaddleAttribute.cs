using System;
using Newtonsoft.Json;

namespace voltaire.Models
{
    public class SaddleAttribute
    {
        [JsonProperty("de_DE")]
        public string DeDe { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("fr_FR")]
        public string FrFr { get; set; }

        [JsonProperty("en_US")]
        public string EnUs { get; set; }
    }
}
