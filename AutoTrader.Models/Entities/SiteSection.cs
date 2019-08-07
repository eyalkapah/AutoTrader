using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class SiteSection
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public SiteSectionRuleContainer[] Rules { get; set; }
    }
}