using System;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;

namespace voltaire.Models
{
    public class SaddleModel
    {
        [JsonProperty("attribute_values")]
        public string AttributeValues { get; set; }

        [JsonProperty("attribute_id")]
        public long AttributeId { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }


        [JsonIgnore]
        public List<string> AttributeValueList
        {
            get
            {
                if (string.IsNullOrEmpty(AttributeValues))
                    return new List<string>();
                else
                {
                    var string_array = AttributeValues.TrimEnd(',').Split(',');
                    return string_array.ToList();
                }
            }
        }
    }
}
