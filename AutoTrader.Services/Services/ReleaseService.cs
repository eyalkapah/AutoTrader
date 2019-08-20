using AutoTrader.Core.Enums;
using AutoTrader.Interfaces.Interfaces;
using AutoTrader.Models.Entities;
using AutoTrader.Models.Enums;
using AutoTrader.Models.Exceptions;
using AutoTrader.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AutoTrader.Services.Services
{
    public class ReleaseService : IReleaseService
    {
        private readonly ICacheService _cacheService;

        public ReleaseService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

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

        public List<ReleaseBase> GetReleases()
        {
            return _cacheService.Categories
        }
    }
}