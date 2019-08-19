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

        public List<Package> GetPackages()
        {
            return _cacheService.Packages;
        }

        public bool IsPackageValid(string packageId, string text)
        {
            var packages = GetPackages();

            var package = packages.FirstOrDefault(p => p.Id == packageId);

            var words = _wordService.GetWords();

            return package.IsPackageValid(words, text);
        }
    }
}