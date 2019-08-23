using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Exceptions
{
    public class UndefinedPackagesException : Exception
    {
        public UndefinedPackagesException() : base("Couldn't fetch packages from cache")
        {
        }
    }
}