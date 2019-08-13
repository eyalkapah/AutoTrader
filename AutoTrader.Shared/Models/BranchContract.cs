using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Shared.Models
{
    public class BranchContract
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("sections")]
        public int Section { get; set; }

        [JsonProperty("bubbleLevel")]
        public int BubbleLevel { get; set; }

        [JsonProperty("raceActivityInSeconds")]
        public int RaceActivityInSeconds { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }
    }
}