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
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("delimiter")]
        public char Delimiter { get; set; }

        [JsonProperty("ruleset")]
        public List<string> RulesSet { get; set; }
    }
}