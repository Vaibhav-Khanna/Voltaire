using System;

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

        public string RemoteId { get; set; }

        [Newtonsoft.Json.JsonProperty("Id")]
        public string Id { get; set; }

        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVersion { get; set; }
    }

}
