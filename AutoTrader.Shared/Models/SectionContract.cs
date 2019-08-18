using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Shared.Models
{
    public class SectionContract
    {
        [JsonProperty("bubbleLevel")]
        public int BubbleLevel { get; set; }

        [JsonProperty("categoryId")]
        public string CategoryId { get; set; }

        [JsonProperty("delimiter")]
        public char Delimiter { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("package")]
        public string PackageId { get; set; }

        [JsonProperty("raceActivityInSeconds")]
        public int RaceActivityInSeconds { get; set; }
    }
}