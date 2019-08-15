using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Exceptions
{
    public class UnknownIrcCommandException : Exception
    {
        public UnknownIrcCommandException(string command) : base($"Unknown irc command: {command}")
        {
        }
    }
}