using AutoTrader.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoTrader.Interfaces.Interfaces
{
    public interface IDataProviderService
    {
        Task<SettingsContract> GetSettingsAsync();
    }
}