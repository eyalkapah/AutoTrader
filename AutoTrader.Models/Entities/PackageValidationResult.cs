using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class PackageValidationResult
    {
        public string PackageId { get; set; }
        public bool? IsValid { get; set; }
    }
}