using AutoTrader.Core.Services;
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

        public async Task<Site> GetSiteAsync(string publisher)
        {
            var sites = await _cacheService.GetSitesAsync();

            return sites.FirstOrDefault(s => s.Name.Equals(publisher, StringComparison.CurrentCultureIgnoreCase));
        }

        public Task<List<Site>> GetSitesAsync()
        {
            return _cacheService.GetSitesAsync();
        }

        public async Task<IEnumerable<Site>> GetSitesAsync(string sectionId)
        {
            var sites = await _cacheService.GetSitesAsync();

            return sites.Where(s => IsEnrolled(s.Enrollments, sectionId));
        }

        private bool IsEnrolled(IEnumerable<Enrollment> enrollments, string sectionId)
        {
            return enrollments.Any(e => e.SectionId.Equals(sectionId));
        }
    }
}