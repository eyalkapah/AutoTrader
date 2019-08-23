using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Exceptions
{
    public class InvalidPackageException : Exception
    {
        public InvalidPackageException(string packageId) : base($"Can't find package with ID: {packageId}")
        {
        }
    }
}