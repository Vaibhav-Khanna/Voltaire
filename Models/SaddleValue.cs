﻿using System;
using Newtonsoft.Json;

namespace voltaire.Models
{
    public class SaddleValue
    {
		[JsonProperty("de_DE")]
        public string DeDe { get; set; }

        [JsonProperty("attribute_id")]
        public long AttributeId { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("fr_FR")]
        public string FrFr { get; set; }

        [JsonProperty("en_US")]
        public string EnUs { get; set; }
    }
}
