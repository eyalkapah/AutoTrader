using AutoTrader.Core.Services;
using AutoTrader.Interfaces.Interfaces;
using AutoTrader.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Services.Services
{
    public class BranchService : IBranchService
    {
        private readonly ICacheService _cacheService;

        public BranchService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<Branch> GetBranchBySectionIdAsync(string sectionId)
        {
            var branches = await _cacheService.GetBranchesAsync();

            return branches.Single(b => b.SectionId.Equals(sectionId));
        }
    }
}