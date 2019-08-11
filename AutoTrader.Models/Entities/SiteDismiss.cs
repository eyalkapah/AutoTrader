using AutoTrader.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class SiteDismiss
    {
        public Site Site { get; set; }
        public DisqualificationType Reason { get; set; }

        public SiteDismiss(Site site, DisqualificationType reason)
        {
            Site = site;
            Reason = reason;
        }
    }
}