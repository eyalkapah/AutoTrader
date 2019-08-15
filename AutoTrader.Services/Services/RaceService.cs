using AutoTrader.Core.Enums;
using AutoTrader.Interfaces.Interfaces;
using AutoTrader.Models.Entities;
using AutoTrader.Models.Exceptions;
using AutoTrader.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Services.Services
{
    public class RaceService : IRaceService
    {
        private readonly ICategoryService _categoryService;
        private readonly IReleaseService _releaseService;
        private readonly ISectionService _sectionService;
        private readonly ISiteService _siteService;
        private readonly IPackageService _packageService;
        private readonly IWordService _wordService;
        private readonly IBranchService _branchService;
        private readonly IPreDbService _preDbService;

        public List<Race> Races { get; set; }

        public RaceService(IReleaseService releaseService, ICategoryService categoryService, ISectionService sectionService, ISiteService siteService, IPackageService packageService, IWordService wordService, IBranchService branchService, IPreDbService preDbService)
        {
            Races = new List<Race>();

            _releaseService = releaseService;
            _categoryService = categoryService;
            _sectionService = sectionService;
            _siteService = siteService;
            _packageService = packageService;
            _wordService = wordService;
            _branchService = branchService;
            _preDbService = preDbService;
        }

        public async Task RaceAsync(string releaseName, string sectionName, IrcPublisher ircPublisher)
        {
            var section = await _sectionService.GetSectionAsync(sectionName);

            if (section == null)
                throw new UnknownSectionException(sectionName);

            var race = GetRace(releaseName, section.Id);

            var site = _siteService.GetSiteAsync(ircPublisher.Channel, ircPublisher.Bot);

            if (site != null)
            {
                if (race == null)
                {
                    // Build new race
                    race = await BuildRaceAsync(releaseName, section, ircPublisher);
                    Races.Add(race);
                }
                else
                {
                    // Participate in existing race
                }
            }
            else
            {
                var preDb = await _preDbService.GetPreDbAsync(ircPublisher.Channel, ircPublisher.Bot);

                if (preDb == null)
                    throw new UnknownPublisherException(ircPublisher.Channel, ircPublisher.Bot);

                if (!preDb.IsEnabled)
                    return;

                if (race != null)
                    return;

                race = await BuildRaceAsync(releaseName, section, ircPublisher);
                Races.Add(race);
            }

            race.RaceAsync();
        }

        public async Task<Race> BuildRaceAsync(string releaseName, Section section, IrcPublisher ircPublisher)
        {
            var categoriesTask = _categoryService.GetCategoryAsync(section.Name);
            var sitesTask = _siteService.GetSitesAsync();
            var packagesTask = _packageService.GetPackagesAsync();
            var wordsTask = _wordService.GetWordsAsync();
            var branchesTask = _branchService.GetBranchBySectionIdAsync(section.Id);

            await categoriesTask;

            var release = await _releaseService.BuildReleaseAsync(releaseName, categoriesTask.Result.Type, section.Delimiter);

            Task.WaitAll(sitesTask, packagesTask, wordsTask, branchesTask);

            var race = new Race(section, release, sitesTask.Result, branchesTask.Result, packagesTask.Result, wordsTask.Result);
            await race.InitAsync();

            return race;
        }

        private Race GetRace(string releaseName, string sectionId)
        {
            return Races.FirstOrDefault(r => r.Release.Name.Equals(releaseName) && r.Section.Id.Equals(sectionId));
        }
    }
}