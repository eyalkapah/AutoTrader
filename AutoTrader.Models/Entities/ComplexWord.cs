using AutoTrader.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class ComplexWord
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Classification { get; set; }
        public List<Word> Words { get; set; }
        public RuleSetPermission Permission { get; set; }
    }
}