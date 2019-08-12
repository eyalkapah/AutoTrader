using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class PreDb
    {
        public string Id { get; set; }

        public string Channel { get; set; }

        public string Bot { get; set; }

        public bool IsEnabled { get; set; }
    }
}