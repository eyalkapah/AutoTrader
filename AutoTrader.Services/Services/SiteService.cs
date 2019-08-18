using AutoTrader.Interfaces.Interfaces;
using AutoTrader.Models.Entities;
using AutoTrader.Models.Enums;
using AutoTrader.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Services.Services
{
    public class SiteService : ISiteService
    {
        private readonly ICacheService _cacheService;

        public SiteService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<Site> GetSiteAsync(string channel, string bot)
        {
            var sites = await _cacheService.GetSitesAsync();

            foreach (var site in sites)
            {
                foreach (var ircInfo in site.IrcInfo)
                {
                    if (ircInfo.IsIrcInfoSame(channel, bot))
                        return site;
                }
            }

            return null;
        }

        public Task<List<Site>> GetSitesAsync()
        {
            return _cacheService.GetSitesAsync();
        }
    }
}