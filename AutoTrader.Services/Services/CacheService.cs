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

        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Section> Sections { get; set; } = new List<Section>();
        public List<Word> Words { get; set; } = new List<Word>();
        public List<Site> Sites { get; set; } = new List<Site>();

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

        public async Task<List<Word>> GetWordsAsync()
        {
            await LoadSettingsIfNeeded();

            return Words;
        }

        public async Task<List<Site>> GetSitesAsync()
        {
            await LoadSettingsIfNeeded();

            return Sites;
        }

        private async Task LoadSettings()
        {
            var settingsContract = await _dataProviderService.GetSettingsAsync();

            Categories = settingsContract.Categories.Select(c => ContractFactory.GetCategory(c)).ToList();

            Sections = Categories.SelectMany(c => c.Sections).ToList();

            Words = settingsContract.Words.Select(w => ContractFactory.GetWord(w)).ToList();

            Sites = settingsContract.Sites.Select(s => ContractFactory.GetSite(s)).ToList();
        }

        private async Task LoadSettingsIfNeeded()
        {
            if (Categories == null || Sections == null || Words == null)
                await LoadSettings();
        }
    }
}