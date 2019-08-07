using AutoTrader.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class Package
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string WordId { get; set; }
        public PackageApplicability Applicability { get; set; }
    }
}