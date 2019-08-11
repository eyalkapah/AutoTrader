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

        public async Task<List<Site>> GetSitesAsync(string sectionId)
        {
            var sites = await _cacheService.GetSitesAsync();

            return sites.Where(s => IsEnrolled(s.Enrollments, sectionId)).ToList();
        }

        public async Task<List<Enrollment>> GetParticipatingSites(string sectionId)
        {
            var sites = await _cacheService.GetSitesAsync();

            return sites.GetParticipatingEnrolls(sectionId);
        }

        private bool IsEnrolled(IEnumerable<Enrollment> enrollments, string sectionId)
        {
            return enrollments.Any(e => e.SectionId.Equals(sectionId));
        }
    }
}