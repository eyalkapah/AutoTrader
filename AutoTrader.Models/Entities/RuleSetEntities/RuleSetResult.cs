using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities.RuleSetEntities
{
    public class RuleSetResult
    {
        public RuleSetBase RuleSet { get; set; }
        public List<string> Matches { get; set; }

        public RuleSetResult()
        {
            Matches = new List<string>();
        }
    }
}