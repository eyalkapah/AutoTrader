using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Exceptions
{
    public class UnknownPublisherException : Exception
    {
        public UnknownPublisherException(string channel, string bot) : base($"Unknown publisher Channel: {channel}, Bot: {bot}")
        {
        }
    }
}