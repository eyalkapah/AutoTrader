using AutoTrader.Core.Enums;
using AutoTrader.Core.Services;
using AutoTrader.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoTrader.Services.Services
{
    public class CacheService : ICacheService
    {
        private readonly DataProviderService _dataProviderService;

        public List<Category> Categories { get; set; }
        public List<Section> Sections { get; set; }

        public CacheService(DataProviderService dataProviderService)
        {
            _dataProviderService = dataProviderService;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            await LoadSettingsIfNeeded();

            return Categories;
        }

        public async Task<List<Section>> GetSectionsAsync()
        {
            await LoadSettingsIfNeeded();

            return Sections;
        }

        private async Task LoadSettings()
        {
            var settingsContract = await _dataProviderService.GetSettingsAsync();

            var categories = new List<Category>();

            foreach (var category in settingsContract.Categories)
            {
                categories.Add(ContractFactory.GetCategory(category));
            }

            Categories = categories;
        }

        private async Task LoadSettingsIfNeeded()
        {
            if (Categories == null)
                await LoadSettings();
        }
    }
}