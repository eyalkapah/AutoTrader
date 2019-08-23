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

        public PackageService(ICacheService cacheService, IWordService wordService)
        {
            _cacheService = cacheService;
            _wordService = wordService;
        }

        public List<Package> GetPackages()
        {
            var packages = _cacheService.Packages;

            if (packages == null)
                throw new UndefinedPackagesException();

            return packages;
        }

        public bool IsPackageValid(string packageId, string text, Dictionary<string, string> contants)
        {
            var packages = GetPackages();

            if (packages == null)
                throw new UndefinedPackagesException();

            var package = packages.FirstOrDefault(p => p.Id == packageId);

            if (package == null)
                throw new InvalidPackageException(packageId);

            var words = _wordService.GetWords();

            return package.IsPackageValid(words, text, contants);
        }
    }
}