using AutoTrader.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class TradeCommand
    {
        public string Bot { get; set; }
        public string Channel { get; set; }
        public TradeCommandType CommandType { get; set; }
        public bool IsValidCommand { get; set; }

        public string ReleaseName { get; set; }

        public string SectionName { get; set; }

        public string Text { get; set; }
    }
}