using AutoTrader.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class StringRuleSet
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public RuleSetType Type { get; set; }
        public List<Word> Words { get; set; }
    }
}