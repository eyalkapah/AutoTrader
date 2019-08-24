using AutoTrader.Interfaces.Interfaces;
using AutoTrader.Models.Entities;
using AutoTrader.Models.Enums;
using AutoTrader.Models.Exceptions;
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
        private readonly IComplexWordService _complexWordService;

        public PackageService(ICacheService cacheService, IWordService wordService, IComplexWordService complexWordService)
        {
            _cacheService = cacheService;
            _wordService = wordService;
            _complexWordService = complexWordService;
        }

        public List<Package> GetPackages()
        {
            var packages = _cacheService.Packages;

            if (packages == null)
                throw new UndefinedException(typeof(Package));

            return packages;
        }

        public bool IsPackageValid(string packageId, string text, Dictionary<string, string> contants)
        {
            var packages = GetPackages();

            var package = packages.FirstOrDefault(p => p.Id == packageId);

            if (package == null)
                throw new InvalidPackageException(packageId);

            var words = _wordService.GetWords();
            var complexWords = _complexWordService.GetComplexWords();

            return package.IsPackageValid(words, complexWords, text, contants);
        }
    }
}