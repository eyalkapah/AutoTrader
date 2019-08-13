using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Exceptions
{
    public class UnknownSiteStatusException : Exception
    {
        public UnknownSiteStatusException(string sitename) : base($"Unknown status for {sitename}")
        {
        }
    }
}