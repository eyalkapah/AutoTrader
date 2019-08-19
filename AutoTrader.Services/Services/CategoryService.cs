using AutoTrader.Core.Enums;
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

        public List<Category> GetCategories()
        {
            return _cacheService.Categories;
        }

        public Category GetCategoryBySectionId(string sectionId)
        {
            var categories = GetCategories();

            return categories.FirstOrDefault(c => c.SectionIds.Contains(sectionId));
        }
    }
}