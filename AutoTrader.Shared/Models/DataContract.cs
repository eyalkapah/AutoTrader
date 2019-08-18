using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Shared.Models
{
    public class DataContract
    {
        [JsonProperty("categories")]
        public CategoryContract[] Categories { get; set; }

        [JsonProperty("complexWords")]
        public ComplexWordContract[] ComplexWords { get; set; }

        [JsonProperty("packages")]
        public PackageContract[] Packages { get; set; }

        [JsonProperty("preDbs")]
        public PreDbContract[] PreDbs { get; set; }

        [JsonProperty("sections")]
        public SectionContract[] Sections { get; set; }

        [JsonProperty("sites")]
        public SiteContract[] Sites { get; set; }

        [JsonProperty("words")]
        public WordContract[] Words { get; set; }
    }
}