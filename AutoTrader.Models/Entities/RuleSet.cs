using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class RuleSet
    {
        public List<StringRuleSet> Strings { get; set; }
        public List<RangeRuleSet> Ranges { get; set; }
    }
}