using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Shared.Models
{
    public class SettingsContract
    {
        [JsonProperty("categories")]
        public CategoryContract[] Categories { get; set; }

        [JsonProperty("sites")]
        public SiteContract[] Sites { get; set; }

        [JsonProperty("words")]
        public WordContract[] Words { get; set; }

        [JsonProperty("complexWords")]
        public ComplexWordContract[] ComplexWords { get; set; }

        [JsonProperty("priorities")]
        public PriorityContract[] Priorities { get; set; }
    }
}