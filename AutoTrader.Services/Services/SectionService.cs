using AutoTrader.Core.Services;
using AutoTrader.Interfaces.Interfaces;
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

        public Task<Section> GetSection(string sectionName)
        {
            return _cacheService.GetSection(sectionName);
        }
    }
}