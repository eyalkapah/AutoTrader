using System.Collections.Generic;
using System.Threading.Tasks;
using AutoTrader.Models.Entities;

namespace AutoTrader.Services.Services
{
    public interface ISiteService
    {
        Task<IEnumerable<Site>> GetSitesAsync(string sectionId);

        Task<List<Site>> GetSitesAsync();
    }
}