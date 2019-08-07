using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class SiteRuleWord
    {
        public int Id { get; set; }
        public int WordId { get; set; }
        public bool IsEnabled { get; set; }
    }
}