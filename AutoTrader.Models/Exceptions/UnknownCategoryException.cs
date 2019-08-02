using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Exceptions
{
    public class UnknownCategoryException : Exception
    {
        public UnknownCategoryException(string sectionName) : base($"Unknown category for section '{sectionName}'")
        {
        }
    }
}