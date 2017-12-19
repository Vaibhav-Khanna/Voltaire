namespace voltaire.Models
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using voltaire.DataStore.Abstraction;

    public class Message : BaseDataObject
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

        [JsonProperty("res_id")] //Document relative Id eg 
        public long ResId { get; set; }

        [JsonProperty("model")] //Related Document Type eg sale.order
        public string Model { get; set; }

        [JsonProperty("message_type")] //Message type: email for email message, notification for system message, comment for other messages such as user replies
        public MessageType MessageType { get; set; }

        [JsonProperty("description")] //Message description: either the subject, or the beginning of the body
        public string Description { get; set; }



    }
    public enum MessageType
    {
        email, notification, comment
    }
}

