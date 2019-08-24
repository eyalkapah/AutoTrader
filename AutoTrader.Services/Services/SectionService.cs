using AutoTrader.Interfaces.Interfaces;
using AutoTrader.Models.Entities;
using AutoTrader.Models.Exceptions;
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

        public Section GetSection(string name)
        {
            var sections = GetSections();

            foreach (var s in sections)
            {
                if (_packageService.IsPackageValid(s.PackageId, name, null))
                    return s;
            }

            return null;
        }

        public IEnumerable<Section> GetSections()
        {
            var sections = _cacheService.Sections;

            if (sections == null)
                throw new UndefinedException(typeof(Section));

            return sections;
        }
    }
}