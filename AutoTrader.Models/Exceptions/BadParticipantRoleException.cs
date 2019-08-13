using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Exceptions
{
    public class BadParticipantRoleException : Exception
    {
        public BadParticipantRoleException(string sitename) : base($"Role for {sitename} cannot be Off at this point")
        {
        }
    }
}