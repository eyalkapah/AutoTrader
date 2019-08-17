using System.Collections.Generic;
using System.Threading.Tasks;
using AutoTrader.Models.Entities;

namespace AutoTrader.Interfaces.Interfaces
{
    public interface IRaceService
    {
        ValueTask RaceAsync(TradeCommand command);
    }
}