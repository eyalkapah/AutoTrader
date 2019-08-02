using AutoTrader.Core.Services;
using AutoTrader.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Services.Services
{
    public class SectionService
    {
        private readonly ICacheService _cacheService;

        public SectionService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<Section> GetSection(string sectionName)
        {
            if (_cacheService.Sections != null)
                return _cacheService.GetSection(sectionName);
        }
    }
}