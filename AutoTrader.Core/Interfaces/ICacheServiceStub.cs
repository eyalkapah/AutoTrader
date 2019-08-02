using System.Collections.Generic;
using System.Threading.Tasks;
using AutoTrader.Core.Models;

namespace AutoTrader.Core.Services
{
    public interface ICacheService
    {
        List<ReleaseCategory> Categories { get; set; }

        Task ReadCache();
    }
}