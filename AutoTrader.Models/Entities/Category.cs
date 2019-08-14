using AutoTrader.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class Category
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public List<string> SectionIds { get; set; }
        public CategoryType Type { get; set; }

        public Category()
        {
            SectionIds = new List<string>();
        }
    }
}