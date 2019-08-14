using AutoTrader.Core.Services;
using AutoTrader.Interfaces.Interfaces;
using AutoTrader.Models.Entities;
using AutoTrader.Models.Enums;
using AutoTrader.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Services.Services
{
    public class PackageService : IPackageService
    {
        private readonly ICacheService _cacheService;
        private readonly IWordService _wordService;

        public PackageService(ICacheService cacheService, IWordService wordService)
        {
            _cacheService = cacheService;
            _wordService = wordService;
        }

        public Task<List<Package>> GetPackagesAsync()
        {
            return _cacheService.GetPackagesAsync();
        }

        public async Task<bool> IsPackageValidAsync(string packageId, string text)
        {
            var packages = await GetPackagesAsync();

            var package = packages.FirstOrDefault(p => p.Id == packageId);

            return package.IsPackageValid(text);

            // Handle a WORD only
            var word = await _wordService.GetWordAsync(package.WordId);

            var result = word.GetMatch(text);

            switch (package.Applicability)
            {
                case PackageApplicability.Must:
                    return result.Pattern.Success && !result.IgnorePattern.Success;

                case PackageApplicability.Banned:
                    return !result.Pattern.Success || result.Pattern.Success && result.IgnorePattern.Success;

                default:
                    throw new NotImplementedException("Unknown applicability");
            }
        }
    }
}