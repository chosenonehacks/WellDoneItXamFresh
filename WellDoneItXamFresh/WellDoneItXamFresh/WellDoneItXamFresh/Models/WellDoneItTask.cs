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
        public WellDoneItTask()
        {

        } 

        [JsonProperty("id")]
        public string Id { get; set; }

        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVersion { get; set; }

        [JsonProperty(PropertyName = "DateUtc")]
        public DateTime DateUtc { get; set; }

        [JsonProperty(PropertyName = "Title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "Complete")]
        public bool Complete { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string DateDisplay { get { return DateUtc.ToLocalTime().ToString("d"); } }

        [Newtonsoft.Json.JsonIgnore]
        public string TimeDisplay { get { return DateUtc.ToLocalTime().ToString("t"); } }
    }
}
