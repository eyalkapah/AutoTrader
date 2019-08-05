using AutoTrader.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities.RuleSetEntities
{
    public class RangeRuleSet : RuleSetBase
    {
        public int Minimum { get; set; }
        public int Maxmimum { get; set; }
    }
}