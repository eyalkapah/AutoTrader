using AutoTrader.Core.Enums;
using AutoTrader.Interfaces.Interfaces;
using AutoTrader.Models.Entities;
using AutoTrader.Models.Entities.Storage;
using AutoTrader.Models.Utils;
using AutoTrader.Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AutoTrader.Services.Services
{
    public class CacheService : ICacheService
    {
        private AppFile _appFile;

        public List<Category> Categories { get; set; }
        public List<ComplexWord> ComplexWords { get; private set; }
        public List<Package> Packages { get; set; }
        public List<PreDb> PreDbs { get; set; }
        public List<Section> Sections { get; set; }
        public List<Site> Sites { get; set; }
        public List<Word> Words { get; set; }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            await LoadSettingsIfNeeded();

            return Categories;
        }

        public async Task<List<Package>> GetPackagesAsync()
        {
            await LoadSettingsIfNeeded();

            return Packages;
        }

        public async Task<List<PreDb>> GetPreDbsAsync()
        {
            await LoadSettingsIfNeeded();

            return PreDbs;
        }

        public async Task<List<Section>> GetSectionsAsync()
        {
            await LoadSettingsIfNeeded();

            return Sections;
        }

        public async Task<List<Site>> GetSitesAsync()
        {
            await LoadSettingsIfNeeded();

            return Sites;
        }

        public async Task<List<Word>> GetWordsAsync()
        {
            await LoadSettingsIfNeeded();

            return Words;
        }

        public async Task LoadCacheAsync(AppFile appFile)
        {
            try
            {
                _appFile = appFile;

                DataContract dataContract = null;

                if (File.Exists(appFile.FullPath))
                {
                    dataContract = await Task.Run(() => JsonHelper.DeserializeJson<DataContract>(appFile.FullPath));
                }
                else
                {
                    if (!File.Exists(appFile.FullPath))
                    {
                        if (!File.Exists(appFile.InstallationFullPath))
                            throw new FileNotFoundException($"Couldn't find default file: {appFile.InstallationFullPath}");

                        dataContract = await Task.Run(() => JsonHelper.DeserializeJson<DataContract>(appFile.InstallationDefaultFolder));

                        Task.Run(() => JsonHelper.SerializeJson(dataContract, appFile.FullPath)).FireAndForget();
                    }
                }

                Categories = dataContract.Categories.Select(c => ContractFactory.GetCategory(c)).ToList();

                Words = dataContract.Words.Select(w => ContractFactory.GetWord(w)).ToList();

                Packages = dataContract.Packages.Select(p => ContractFactory.GetPackage(p)).ToList();

                Sections = dataContract.Sections.Select(s => ContractFactory.GetSection(s)).ToList();

                ComplexWords = dataContract.ComplexWords.Select(c => ContractFactory.GetComplexWord(c)).ToList();

                Sites = dataContract.Sites.Select(s => ContractFactory.GetSite(s, Packages)).ToList();

                PreDbs = dataContract.PreDbs.Select(p => ContractFactory.GetPreDb(p)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task LoadSettingsIfNeeded()
        {
            if (Categories == null || Sections == null || Words == null)
                await LoadCacheAsync(_appFile);
        }
    }
}