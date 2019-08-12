using AutoTrader.Core.Enums;
using AutoTrader.Interfaces.Interfaces;
using AutoTrader.Models.Entities;
using AutoTrader.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Services.Services
{
    public class RaceService
    {
        private readonly ICategoryService _categoryService;
        private readonly IReleaseService _releaseService;
        private readonly ISectionService _sectionService;
        private readonly ISiteService _siteService;

        public RaceService(IReleaseService releaseService, ICategoryService categoryService, ISectionService sectionService, ISiteService siteService)
        {
            _releaseService = releaseService;
            _categoryService = categoryService;
            _sectionService = sectionService;
            _siteService = siteService;
        }

        public async Task BuildRace(string releaseName, string sectionName, string publisher)
        {
            var category = await _categoryService.GetCategoryAsync(sectionName);
            var section = await _sectionService.GetSectionAsync(sectionName);
            var sites = await _siteService.GetSitesAsync();

            var release = await _releaseService.BuildReleaseAsync(releaseName, category.Type, section.Delimiter);

            var race = new Race(section, release, sites);
            race.FilterNonSectionSites();
            race.FilterOffStatusSites();
            race.FilterAffiliateUploadOnly();
            race.BuildParticipantsQueue();

            var site = _siteService.GetSiteAsync(publisher);
        }

        public async Task StartRaceAsync(Race race)
        {
            var participant = race.GetSourceSite();
        }
    }
}