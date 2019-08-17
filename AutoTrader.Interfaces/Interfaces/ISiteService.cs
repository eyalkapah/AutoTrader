using System.Collections.Generic;
using System.Threading.Tasks;
using AutoTrader.Models.Entities;

namespace AutoTrader.Services.Services
{
    public interface ISiteService
    {
        Task<Site> GetSiteAsync(string channel, string bot);

        Task<List<Site>> GetSitesAsync();
    }
}