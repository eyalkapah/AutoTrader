using System.Collections.Generic;
using System.Threading.Tasks;
using AutoTrader.Models.Entities;

namespace AutoTrader.Interfaces.Interfaces
{
    public interface IPreDbService
    {
        Task<PreDb> GetPreDbAsync(string channel, string bot);

        Task<List<PreDb>> GetPreDbsAsync();
    }
}