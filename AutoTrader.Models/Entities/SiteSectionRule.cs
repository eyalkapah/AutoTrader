using AutoTrader.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class SiteSectionRule
    {
        public string Id { get; set; }
        public string WordId { get; set; }
        public bool IsEnabled { get; set; }
        public RuleApplicability RuleApplicability { get; set; }
    }
}