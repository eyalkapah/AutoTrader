using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class Race
    {
        public Section Section { get; set; }
        public ReleaseBase Release { get; set; }
        public List<SiteDismiss> DismissedSites { get; set; }

        public Race()
        {
            DismissedSites = new List<SiteDismiss>();
        }
    }
}