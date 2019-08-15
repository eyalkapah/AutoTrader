using AutoTrader.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class IrcCommand
    {
        public string Text { get; set; }
        public IrcCommandType CommandType { get; set; }
        public string Channel { get; set; }
        public string Bot { get; set; }
        public string ReleaseName { get; set; }
        public string SectionName { get; set; }
        public bool IsValidCommand { get; set; }
    }
}