using System;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;

namespace voltaire.Models
{
    public class SaddleModel
    {
        [JsonProperty("attribute_name")]
        public string AttributeName { get; set; }

        [JsonProperty("attribute_id")]
        public long AttributeId { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("attribute_values_ids")]
        public string AttributeValuesIds { get; set; }

        [JsonProperty("saddle_name")]
        public string SaddleName { get; set; }


        [JsonIgnore]
        public List<string> AttributeValueList
        {
            get
            {
                if (string.IsNullOrEmpty(AttributeValuesIds))
                    return new List<string>();
                else
                {
                    var string_array = AttributeValuesIds.TrimEnd(',').Split(',');
                    return string_array.ToList();
                }
            }
        }
    }
}
