using AutoTrader.Interfaces.Interfaces;
using AutoTrader.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Services.Services
{
    public class SectionService : ISectionService
    {
        private readonly ICacheService _cacheService;
        private readonly IPackageService _packageService;

        public SectionService(ICacheService cacheService, IPackageService packageService)
        {
            _cacheService = cacheService;
            _packageService = packageService;
        }

        public async Task<Section> GetSectionAsync(string name)
        {
            var sections = await _cacheService.GetSectionsAsync();

            foreach (var s in sections)
            {
                if (await _packageService.IsPackageValidAsync(s.PackageId, name))
                    return s;
            }

            return null;
        }
    }
}