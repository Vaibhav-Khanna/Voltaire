using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Syncfusion.Pdf;
using voltaire.DataStore.Abstraction;

namespace voltaire.Models
{
    public class Contract : BaseDataObject
    {
       
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }


        [JsonProperty("order_number")]
        public string OrderNumber { get; set; }

        [JsonProperty("period_begin")]
        public DateTime? PeriodBegin { get; set; }

        [JsonProperty("period_end")]
        public DateTime? PeriodEnd { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("partner_id")]
        public long PartnerId { get; set; }

        [JsonProperty("to_send")]
        public bool ToSend { get; set; }

        [JsonIgnore]
        public byte[] SignImageSource { get; set; }

        [JsonIgnore]
        public PdfDocument Document { get; set; }

    }
}
