namespace voltaire.Models.DataObjects
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using voltaire.DataStore.Abstraction;

    public class Event : BaseDataObject
    {
      
        //[JsonProperty("version")]
        //public string Version { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("externalId")]
        public string ExternalId { get; set; }

        [JsonProperty("write_date")]
        public DateTime WriteDate { get; set; }

        [JsonProperty("start_datetime")]
        public DateTime StartDatetime { get; set; }

        [JsonProperty("stop")]
        public DateTime Stop { get; set; }

        [JsonProperty("stop_datetime")]
        public DateTime StopDatetime { get; set; }

        [JsonProperty("duration")]
        public long Duration { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("alarm_id")]
        public long AlarmId { get; set; }
    }
}
