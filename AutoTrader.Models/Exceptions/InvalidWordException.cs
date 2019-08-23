using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Exceptions
{
    public class InvalidWordException : Exception
    {
        public InvalidWordException(string packageId, string wordId) : base($"Can't find word {wordId} for package {packageId}")
        {
        }
    }
}