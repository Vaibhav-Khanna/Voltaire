namespace voltaire.Models
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using voltaire.DataStore.Abstraction;

    public class Message: BaseDataObject
    {
       
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("externalId")]
        public long ExternalId { get; set; }

        [JsonProperty("write_date")]
        public DateTime WriteDate { get; set; }

        [JsonProperty("author_id")]
        public long AuthorId { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }
}

