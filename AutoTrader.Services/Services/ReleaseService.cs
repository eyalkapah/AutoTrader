using AutoTrader.Core.Enums;
using AutoTrader.Interfaces.Interfaces;
using AutoTrader.Models.Entities;
using AutoTrader.Models.Exceptions;
using AutoTrader.Models.Extensions;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AutoTrader.Services.Services
{
    public class ReleaseService
    {
        private readonly ICategoryService _categoryService;
        private readonly ISectionService _sectionService;

        public ReleaseService(ICategoryService categoryService, ISectionService sectionService)
        {
            _categoryService = categoryService;
            _sectionService = sectionService;
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

            switch (category.Type)
            {
                case CategoryType.Audio:
                    var audioRelease = new AudioRelease(releaseName);
                    audioRelease.ExtractGroup();
                    audioRelease.ExtractArtistAndTitle(section.Delimiter);

                    break;

                case CategoryType.Unknown:
                    break;
            }
        }
    }
}