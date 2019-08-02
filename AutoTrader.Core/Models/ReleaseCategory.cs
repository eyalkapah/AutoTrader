using AutoTrader.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Core.Models
{
    public class ReleaseCategory
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ReleaseCategoryType Type { get; set; }
        public List<ReleaseSection> Sections { get; set; }
    }
}