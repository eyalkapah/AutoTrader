using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Exceptions
{
    public class InvalidReleaseFormatException : Exception
    {
        public InvalidReleaseFormatException(string errorMessage) : base(errorMessage)
        {
        }
    }
}