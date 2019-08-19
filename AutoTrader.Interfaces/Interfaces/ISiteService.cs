using System.Collections.Generic;
using System.Threading.Tasks;
using AutoTrader.Models.Entities;

namespace AutoTrader.Interfaces.Interfaces
{
    public interface ISiteService
    {
        Site GetSite(string channel, string bot);

        List<Site> GetSites();
    }
}