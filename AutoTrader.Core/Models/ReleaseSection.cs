using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Core.Models
{
    public class ReleaseSection
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ReleaseSectionRuleSet> RuleSet { get; set; }

        public ReleaseSection()
        {
            RuleSet = new List<ReleaseSectionRuleSet>();
        }
    }
}