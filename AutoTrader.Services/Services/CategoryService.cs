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

        public async Task<Category> GetCategoryAsync(string sectionName)
        {
            var categories = await GetCateogoriesAsync();

            foreach (var category in categories)
            {
                if (category.Sections.Any(s => s.Name.Equals(sectionName, StringComparison.CurrentCultureIgnoreCase)))
                    return category;
            }

            return null;
        }

        public async Task<CategoryType> GetCategoryType(string sectionName)
        {
            var categories = await _cacheService.GetCategoriesAsync();

            foreach (var category in categories)
            {
                if (category.Sections.Any(s => s.Name.Equals(sectionName, StringComparison.CurrentCultureIgnoreCase)))
                    return category.Type;
            }

            throw new UnknownCategoryException(sectionName);
        }

        public Task<List<Category>> GetCateogoriesAsync()
        {
            return _cacheService.GetCategoriesAsync();
        }
    }
}