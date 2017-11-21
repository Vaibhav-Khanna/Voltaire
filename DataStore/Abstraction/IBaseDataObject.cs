using System;
using Newtonsoft.Json;

namespace voltaire.DataStore.Abstraction
{
    
    public interface IBaseDataObject
    {
        string Id { get; set; }
    }

    public class BaseDataObject : IBaseDataObject
    {
     
        public BaseDataObject()
        {
            Id = Guid.NewGuid().ToString();
        }

        [JsonIgnore]
        public string RemoteId { get; set; }

        [Newtonsoft.Json.JsonProperty("id")]
        public string Id { get; set; }

        //[Microsoft.WindowsAzure.MobileServices.Version]
        //public string AzureVersion { get; set; }
    }

}
