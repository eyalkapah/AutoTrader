using AutoTrader.Core.Enums;
using AutoTrader.Core.Exceptions;
using AutoTrader.Core.Models.Release;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Core.Services
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

                var categoryType = await _categoryService.GetCategoryType(sectionName);

                switch (categoryType)
                {
                    case ReleaseCategoryType.Audio:
                        var audioRelease = new AudioRelease(releaseName);

                        break;

                    case ReleaseCategoryType.Unknown:
                        break;

                    default:
                        break;
                }
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
    }
}