using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Exceptions
{
    public class UnknownReleaseCategoryException : Exception
    {
        public UnknownReleaseCategoryException(string releaseName, string categoryName) : base($"Unknown category '{categoryName}' accepted for '{releaseName}'")
        {
        }
    }
}