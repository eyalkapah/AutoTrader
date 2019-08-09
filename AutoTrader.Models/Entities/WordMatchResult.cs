using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class WordMatchResult
    {
        public Match IgnorePattern { get; set; }
        public Match Pattern { get; set; }
    }
}