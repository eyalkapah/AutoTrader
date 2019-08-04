using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Shared.Models
{
    public class RuleSetContract
    {
        [JsonProperty("strings")]
        public List<StringRuleSetContract> Strings { get; set; }

        [JsonProperty("ranges")]
        public List<RangeRuleSetContract> Ranges { get; set; }
    }
}