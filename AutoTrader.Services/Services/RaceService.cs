﻿using AutoTrader.Core.Enums;
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

        public List<Race> Races { get; set; }

        public RaceService(IReleaseService releaseService, ICategoryService categoryService, ISectionService sectionService, ISiteService siteService)
        {
            Races = new List<Race>();

            _releaseService = releaseService;
            _categoryService = categoryService;
            _sectionService = sectionService;
            _siteService = siteService;
        }

        public async Task RaceAsync(string releaseName, string sectionName, IrcPublisher publisher)
        {
            var section = await _sectionService.GetSectionAsync(sectionName);

            if (section == null)
                return;

            var race = GetRace(releaseName, section.Id);

            var site = _siteService.GetSiteAsync(publisher);

            if (site != null)
            {
            }

            if (race == null)
            {
                race = await BuildRaceAsync(releaseName, section, publisher);
                Races.Add(race);
            }
            else
            {
            }
        }

        public async Task<Race> BuildRaceAsync(string releaseName, Section section, string publisher)
        {
            var category = await _categoryService.GetCategoryAsync(section.Name);
            var sites = await _siteService.GetSitesAsync();

            var release = await _releaseService.BuildReleaseAsync(releaseName, category.Type, section.Delimiter);

            var race = new Race(section, release, sites);
            race.FilterNonSectionSites();
            race.FilterOffStatusSites();
            race.FilterAffiliateUploadOnly();
            race.BuildParticipantsQueue();

            return race;
        }

        public async Task StartRaceAsync(Race race)
        {
            var participant = race.GetSourceSite();
        }

        private Race GetRace(string releaseName, string sectionId)
        {
            return Races.FirstOrDefault(r => r.Release.Name.Equals(releaseName) && r.Section.Id.Equals(sectionId));
        }
    }
}