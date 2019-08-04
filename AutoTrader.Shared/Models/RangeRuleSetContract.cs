using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Shared.Models
{
    public class RangeRuleSetContract
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("minimum")]
        public int Minimum { get; set; }

        [JsonProperty("maximum")]
        public int Maxmimum { get; set; }

        [JsonProperty("permission")]
        public int Permission { get; set; }
    }
}