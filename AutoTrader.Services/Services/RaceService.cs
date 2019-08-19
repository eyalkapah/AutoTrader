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
        private readonly ICategoryService _categoryService;
        private readonly IPackageService _packageService;
        private readonly IPreDbService _preDbService;

        private readonly IReleaseService _releaseService;
        private readonly ISectionService _sectionService;
        private readonly ISiteService _siteService;
        private readonly IWordService _wordService;
        private RaceManager _raceManager;

        public RaceService(IReleaseService releaseService, ICategoryService categoryService, ISectionService sectionService, ISiteService siteService, IPackageService packageService, IWordService wordService, IPreDbService preDbService)
        {
            _raceManager = new RaceManager();
            _releaseService = releaseService;
            _categoryService = categoryService;
            _sectionService = sectionService;
            _siteService = siteService;
            _packageService = packageService;
            _wordService = wordService;
            _preDbService = preDbService;
        }

        public async ValueTask RaceAsync(TradeCommand command)
        {
            var section = _sectionService.GetSection(command.SectionName);

            if (section == null)
                throw new UnknownSectionException(command.SectionName);

            var race = _raceManager.GetRace(command.ReleaseName);

            if (race.Status == RaceStatus.Completed)
                return;

            var site = _siteService.GetSite(command.Channel, command.Bot);

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
                var preDb = _preDbService.GetPreDb(command.Channel, command.Bot);

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
            var category = _categoryService.GetCategoryBySectionId(section.Id);

            var release = await _releaseService.BuildReleaseAsync(command.ReleaseName, category.Type, section.Delimiter);

            var race = new Race(section, release, _siteService.GetSites(), _packageService.GetPackages(), _wordService.GetWords());
            await race.InitAsync();

            return race;
        }
    }
}