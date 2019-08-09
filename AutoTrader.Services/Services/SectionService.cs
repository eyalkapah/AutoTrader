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
    public class SectionService : ISectionService
    {
        private readonly ICacheService _cacheService;

        public SectionService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<Section> GetSection(string name)
        {
            var sections = await _cacheService.GetSectionsAsync();

            foreach (var section in sections)
            {
                var package = section.Package;
            }
            return sections.FirstOrDefault(s => s.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}