using AutoTrader.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities.RuleSetEntities
{
    public class RuleSetBase
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public RuleSetType Type { get; set; }
    }
}