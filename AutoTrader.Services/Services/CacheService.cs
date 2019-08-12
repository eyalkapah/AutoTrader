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
        public List<ComplexWord> ComplexWords { get; private set; }
        public List<Site> Sites { get; set; } = new List<Site>();
        public List<Priority> Priorities { get; set; } = new List<Priority>();
        public List<PreDb> PreDbs { get; set; } = new List<PreDb>();

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

        public async Task<List<PreDb>> GetPreDbsAsync()
        {
            await LoadSettingsIfNeeded();

            return PreDbs;
        }

        private async Task LoadSettings()
        {
            var settingsContract = await _dataProviderService.GetSettingsAsync();

            Categories = settingsContract.Categories.Select(c => ContractFactory.GetCategory(c)).ToList();

            Sections = Categories.SelectMany(c => c.Sections).ToList();

            Words = settingsContract.Words.Select(w => ContractFactory.GetWord(w)).ToList();

            ComplexWords = settingsContract.ComplexWords.Select(c => ContractFactory.GetComplexWord(c)).ToList();

            Sites = settingsContract.Sites.Select(s => ContractFactory.GetSite(s)).ToList();

            Priorities = settingsContract.Priorities.Select(p => ContractFactory.GetPriority(p)).ToList();

            PreDbs = settingsContract.PreDbs.Select(p => ContractFactory.GetPreDb(p)).ToList();
        }

        private async Task LoadSettingsIfNeeded()
        {
            if (Categories == null || Sections == null || Words == null)
                await LoadSettings();
        }
    }
}