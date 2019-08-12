using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class Priority
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Rank { get; set; }

        public List<PrioritySection> Sections { get; set; }
    }
}