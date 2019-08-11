using AutoTrader.Core.Enums;
using AutoTrader.Interfaces.Interfaces;
using AutoTrader.Models.Entities;
using AutoTrader.Models.Enums;
using AutoTrader.Models.Exceptions;
using AutoTrader.Models.Extensions;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AutoTrader.Services.Services
{
    public class ReleaseService
    {
        private readonly ICategoryService _categoryService;
        private readonly ISectionService _sectionService;
        private readonly ISiteService _siteService;

        public ReleaseService(ICategoryService categoryService, ISectionService sectionService, ISiteService siteService)
        {
            _categoryService = categoryService;
            _sectionService = sectionService;
            _siteService = siteService;
        }

        public async Task BuildReleaseAsync(string releaseName, string sectionName)
        {
            try
            {
                if (string.IsNullOrEmpty(releaseName))
                    throw new ArgumentNullException("releaseName");

                if (string.IsNullOrEmpty(sectionName))
                    throw new ArgumentNullException(sectionName);

                await ProcessRelease(releaseName, sectionName);
            }
            catch (UnknownReleaseCategoryException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task ProcessRelease(string releaseName, string sectionName)
        {
            var category = await _categoryService.GetCategoryAsync(sectionName);
            var section = await _sectionService.GetSection(sectionName);
            var sites = await _siteService.GetParticipatingSites(section.Id);

            switch (category.Type)
            {
                case CategoryType.Audio:
                    var audioRelease = new AudioRelease(releaseName);
                    audioRelease.ExtractGroup();
                    audioRelease.ExtractArtistAndTitle(section.Delimiter);

                    var publishers = sites.
                    }
            Parallel.ForEach(section.RuleSet.Strings, rule =>
            {
                rule.ProcessStringRuleSet(releaseName);
            });

            Parallel.ForEach(section.RuleSet.Ranges, rule =>
            {
                rule.ProcessRuleSet(releaseName);
            });

            break;

                case CategoryType.Unknown:
                    break;
        }
    }
}
}