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

        public Site GetSite(string channel, string bot)
        {
            var sites = _cacheService.Sites;

            foreach (var site in sites)
            {
                if (site.IsIrcInfo(channel, bot))
                    return site;
            }

            return null;
        }

        public List<Site> GetSites()
        {
            return _cacheService.Sites;
        }
    }
}