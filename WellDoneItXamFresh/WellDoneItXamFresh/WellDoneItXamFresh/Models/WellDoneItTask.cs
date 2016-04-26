using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PropertyChanged;

namespace WellDoneIt.Model
{
    [ImplementPropertyChanged]
    public class WellDoneItTask
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVersion { get; set; }

        [JsonProperty(PropertyName = "dateUtc")]
        public DateTime DateUtc { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "complete")]
        public bool Complete { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string DateDisplay { get { return DateUtc.ToLocalTime().ToString("d"); } }

        [Newtonsoft.Json.JsonIgnore]
        public string TimeDisplay { get { return DateUtc.ToLocalTime().ToString("t"); } }

        [Newtonsoft.Json.JsonProperty("userId")]
        public string UserId { get; set; }

        [Newtonsoft.Json.JsonProperty("context")]
        public string Context { get; set; }
    }
}
