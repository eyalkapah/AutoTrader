using AutoTrader.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class Word
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Classification { get; set; }
        public string Pattern { get; set; }
        public string IgnorePattern { get; set; }
        public string ForbiddenPattern { get; set; }
        public RuleSetPermission Permission { get; set; }
    }
}