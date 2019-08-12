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
    public class ReleaseService : IReleaseService
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

        //public async Task BuildReleaseAsync(string releaseName, string sectionName)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(releaseName))
        //            throw new ArgumentNullException("releaseName");

        //        if (string.IsNullOrEmpty(sectionName))
        //            throw new ArgumentNullException(sectionName);

        //        await ProcessRelease(releaseName, sectionName);
        //    }
        //    catch (UnknownReleaseCategoryException ex)
        //    {
        //        Debug.WriteLine(ex.Message);
        //    }
        //    catch (ArgumentNullException ex)
        //    {
        //        Debug.WriteLine(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        public async Task<ReleaseBase> BuildReleaseAsync(string releaseName, CategoryType categoryType, char delimiter)
        {
            ReleaseBase release = null;

            switch (categoryType)
            {
                case CategoryType.Audio:
                    release = new AudioRelease(releaseName);

                    await Task.Run(() =>
                    {
                        release.ExtractGroup();
                        ((AudioRelease)release).ExtractArtistAndTitle(delimiter);
                    });
                    break;

                case CategoryType.Video:
                    break;

                case CategoryType.Data:
                    break;

                case CategoryType.Unknown:
                    throw new NotImplementedException("Unknown category, failed to build release");
            }

            return release;
        }
    }
}