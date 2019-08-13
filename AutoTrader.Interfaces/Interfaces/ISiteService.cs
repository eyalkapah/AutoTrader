﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoTrader.Models.Entities;

namespace AutoTrader.Services.Services
{
    public interface ISiteService
    {
        Task<Site> GetSiteAsync(string publisher);

        Task<IEnumerable<Site>> GetSitesAsync(string sectionId);

        Task<List<Site>> GetSitesAsync();

        Task<Site> GetSiteAsync(string channel, string bot);
    }
}