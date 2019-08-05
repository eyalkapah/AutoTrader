using AutoTrader.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities.RuleSetEntities
{
    public class StringRuleSet : RuleSetBase
    {
        public List<Word> Words { get; set; }
    }
}