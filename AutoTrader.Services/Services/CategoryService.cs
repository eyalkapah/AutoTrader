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
        private readonly DataProviderService _dataProviderService;

        public CategoryService(ICacheService cacheService, DataProviderService dataProviderService)
        {
            _cacheService = cacheService;
            _dataProviderService = dataProviderService;
        }

        public Task<CategoryType> GetCategoryType(string sectionName)
        {
            foreach (var category in _cacheService.Categories)
            {
                if (category.Sections.Any(s => s.Name.Equals(sectionName, StringComparison.CurrentCultureIgnoreCase)))
                {
                    return Task.FromResult(category.Type);
                }
            }

            throw new UnknownCategoryException(sectionName);
        }

        public async Task<List<Category>> GetCateogories()
        {
            if (_cacheService.Categories != null)
                return _cacheService.Categories;

            var contracts = await _dataProviderService.GetCategoriesAsync();

            var categories = new List<Category>();

            foreach (var category in contracts)
            {
                categories.Add(ContractFactory.GetReleaseCategory(category));
            }

            _cacheService.Categories = categories;

            return categories;
        }
    }
}