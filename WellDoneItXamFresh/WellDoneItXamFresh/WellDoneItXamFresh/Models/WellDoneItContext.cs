using System;
using Newtonsoft.Json;
using PropertyChanged;

namespace WellDoneIt.Model
{
    [ImplementPropertyChanged]
    public class WellDoneItContext
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVersion { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("userId")]
        public string UserId { get; set; }
        
    }
}