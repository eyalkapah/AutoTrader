using AutoTrader.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class RuleSet
    {
        public string Name { get; set; }
        public RuleSetType Type { get; set; }
        public List<Word> Words { get; set; }
        public string Id { get; set; }
    }
}