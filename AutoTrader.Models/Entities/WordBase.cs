using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class WordBase
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Classification { get; set; }
    }
}