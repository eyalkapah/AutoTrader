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

        public async Task<List<Category>> GetCategories()
        {
            await LoadSettingsIfNeeded();

            return Categories;
        }

        public async Task<Section> GetSection(string name)
        {
            await LoadSettingsIfNeeded();

            return Sections.FirstOrDefault(s => s.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
        }

        private async Task LoadSettingsIfNeeded()
        {
            if (Categories == null)
                await LoadSettings();
        }

        public async Task LoadSettings()
        {
            var settingsContract = await _dataProviderService.GetSettingsAsync();

            var categories = new List<Category>();
            var ruleSet = new List<RuleSet>();

            foreach (var category in settingsContract.Categories)
            {
                categories.Add(ContractFactory.GetReleaseCategory(category));
            }

            foreach (var rule in settingsContract.RuleSet)
            {
                ruleSet.Add(ContractFactory.GetRule(rule));
            }

            Categories = categories;
        }
    }
}