using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class Section
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public RuleSet RuleSet { get; set; }
        public char Delimiter { get; set; }
    }
}