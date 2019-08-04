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

        [JsonProperty("rulesets")]
        public RuleSetContract RuleSet { get; set; }
    }
}