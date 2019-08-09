using AutoTrader.Interfaces.Interfaces;
using AutoTrader.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Services.Services
{
    public class PackageService
    {
        private readonly IWordService _wordService;

        public PackageService(IWordService wordService)
        {
            _wordService = wordService;
        }

        public bool IsPackageValid(Package package)
        {
        }
    }
}