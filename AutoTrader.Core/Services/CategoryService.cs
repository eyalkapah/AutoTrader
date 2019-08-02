using AutoTrader.Core.Enums;
using AutoTrader.Core.Exceptions;
using AutoTrader.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICacheService _cacheService;

        public CategoryService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public Task<ReleaseCategoryType> GetCategoryType(string sectionName)
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
    }
}