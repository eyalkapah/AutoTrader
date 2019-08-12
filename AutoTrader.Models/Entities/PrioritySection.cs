using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class PrioritySection
    {
        public int Id { get; set; }

        public bool IsEnabled { get; set; }

        public string[] SitesIds { get; set; }
    }
}