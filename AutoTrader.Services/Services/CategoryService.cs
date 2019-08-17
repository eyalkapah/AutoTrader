using AutoTrader.Core.Enums;
using AutoTrader.Core.Services;
using AutoTrader.Interfaces.Interfaces;
using AutoTrader.Models.Entities;
using AutoTrader.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoTrader.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICacheService _cacheService;

        public CategoryService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<Category> GetCategoryBySectionIdAsync(string sectionId)
        {
            var categories = await GetCateogoriesAsync();

            return categories.FirstOrDefault(c => c.SectionIds.Contains(sectionId));
        }

        public Task<List<Category>> GetCateogoriesAsync()
        {
            return _cacheService.GetCategoriesAsync();
        }
    }
}