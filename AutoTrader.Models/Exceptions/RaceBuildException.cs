using AutoTrader.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Exceptions
{
    public class RaceBuildException : Exception
    {
        public RaceBuildException(TradeCommand command) : base($"Failed to build race for {command.ReleaseName}")
        {
        }
    }
}