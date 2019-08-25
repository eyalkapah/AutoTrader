using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Exceptions
{
    public class UnknownWordException : Exception
    {
        public UnknownWordException(string wordId) : base($"Can't find word with ID: {wordId}")
        {
        }
    }
}