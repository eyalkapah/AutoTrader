using AutoTrader.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class ComplexWord : WordBase
    {
        public List<string> WordIds { get; set; }

        public ComplexWord()
        {
            WordIds = new List<string>();
        }
    }
}