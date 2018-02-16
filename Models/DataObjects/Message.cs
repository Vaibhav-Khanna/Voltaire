namespace voltaire.Models
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using voltaire.DataStore.Abstraction;

    public class Message : BaseDataObject
    {

        //[JsonProperty("version")]
        //public string Version { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("external_id")]
        public long ExternalId { get; set; }

        [JsonProperty("write_date")]
        public System.DateTime WriteDate { get; set; }

        [JsonProperty("author_id")]
        public string AuthorId { get; set; }

        [JsonProperty("external_author_id")]
        public long ExternalAuthorId { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("date")]
        public System.DateTime Date { get; set; }

        [JsonProperty("subtype_id")]
        public long SubtypeId { get; set; }

        [JsonProperty("res_id")]
        public string ResId { get; set; }

        [JsonProperty("external_res_id")]
        public long ExternalResId { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("message_type")]
        public string MessageType { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }


    }
    public enum MessageType
    {
        email, notification, comment
    }
}

