using System;
using Newtonsoft.Json;

namespace voltaire.Models
{
    public class LegalFilesModel
    {	
        [JsonProperty("termAndConditions")]
        public string TermAndConditions { get; set; } 
    }
}
