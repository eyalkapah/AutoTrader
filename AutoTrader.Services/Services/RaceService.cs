using AutoTrader.Core.Enums;
using AutoTrader.Interfaces.Interfaces;
using AutoTrader.Models.Entities;
using AutoTrader.Models.Enums;
using AutoTrader.Models.Exceptions;
using AutoTrader.Models.Extensions;
using AutoTrader.Models.Utils;
using AutoTrader.Services.Managers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Services.Services
{
    public class RaceService : IRaceService
    {
        private readonly IBranchService _branchService;
        private readonly ICategoryService _categoryService;
        private readonly IPackageService _packageService;
        private readonly IPreDbService _preDbService;

        private readonly IReleaseService _releaseService;
        private readonly ISectionService _sectionService;
        private readonly ISiteService _siteService;
        private readonly IWordService _wordService;
        private RaceManager _raceManager;

        public RaceService(IReleaseService releaseService, ICategoryService categoryService, ISectionService sectionService, ISiteService siteService, IPackageService packageService, IWordService wordService, IBranchService branchService, IPreDbService preDbService)
        {
            _raceManager = new RaceManager();
            _releaseService = releaseService;
            _categoryService = categoryService;
            _sectionService = sectionService;
            _siteService = siteService;
            _packageService = packageService;
            _wordService = wordService;
            _branchService = branchService;
            _preDbService = preDbService;
        }

        public async ValueTask RaceAsync(TradeCommand command)
        {
            var section = await _sectionService.GetSectionAsync(command.SectionName);

            if (section == null)
                throw new UnknownSectionException(command.SectionName);

            var race = _raceManager.GetRace(command.ReleaseName);

            if (race.Status == RaceStatus.Completed)
                return;

            var site = await _siteService.GetSiteAsync(command.Channel, command.Bot);

            if (site != null)
            {
                if (race == null)
                {
                    // Build new race
                    race = await BuildRaceAsync(command, section);

                    if (race == null)
                        throw new RaceBuildException(command);

                    _raceManager.AddRace(race);
                }
                else
                {
                    // Participate in existing race
                }
            }
            else
            {
                var preDb = await _preDbService.GetPreDbAsync(command.Channel, command.Bot);

                if (preDb == null)
                    throw new UnknownPublisherException(command.Channel, command.Bot);

                if (!preDb.IsEnabled)
                    return;

                if (race != null)
                    return;

                race = await BuildRaceAsync(command, section);

                if (race == null)
                    throw new RaceBuildException(command);

                _raceManager.AddRace(race);
            }

            race.StartRace(site);
        }

        private async Task<Race> BuildRaceAsync(TradeCommand command, Section section)
        {
            var categoriesTask = _categoryService.GetCategoryBySectionIdAsync(section.Id);
            var sitesTask = _siteService.GetSitesAsync();
            var packagesTask = _packageService.GetPackagesAsync();
            var wordsTask = _wordService.GetWordsAsync();
            var branchesTask = _branchService.GetBranchBySectionIdAsync(section.Id);

            await categoriesTask;

            var release = await _releaseService.BuildReleaseAsync(command.ReleaseName, categoriesTask.Result.Type, section.Delimiter);

            Task.WaitAll(sitesTask, packagesTask, wordsTask, branchesTask);

            var race = new Race(section, release, sitesTask.Result, branchesTask.Result, packagesTask.Result, wordsTask.Result);
            await race.InitAsync();

            return race;
        }
    }
}