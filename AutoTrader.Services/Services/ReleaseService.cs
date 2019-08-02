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

        public ReleaseService(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task BuildReleaseAsync(string releaseName, string sectionName)
        {
            try
            {
                if (string.IsNullOrEmpty(releaseName))
                    throw new ArgumentNullException("releaseName");

                if (string.IsNullOrEmpty(sectionName))
                    throw new ArgumentNullException(sectionName);

                await ProcessRelease(releaseName, sectionName)
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

            return Task.CompletedTask;
        }

        public async Task ProcessRelease(string releaseName, string sectionName)
        {
            var categoryType = await _categoryService.GetCategoryType(sectionName);

            switch (categoryType)
            {
                case CategoryType.Audio:
                    var audioRelease = new AudioRelease(releaseName);
                    audioRelease.ProcessRelease()

                    break;

                case CategoryType.Unknown:
                    break;
            }
        }
    }
}