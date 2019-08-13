using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class Branch
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int SectionId { get; set; }

        public int BubbleLevel { get; set; }

        public bool IsEnabled { get; set; }
        public int RaceActivityInSeconds { get; set; }
    }
}