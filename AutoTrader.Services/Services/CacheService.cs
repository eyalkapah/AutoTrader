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

        private List<Category> _categories;
        private List<ComplexWord> _complexWords;
        private List<Package> _packages;
        private List<PreDb> _preDbs;
        private List<Section> _sections;
        private List<Site> _sites;
        private List<Word> _words;

        public async Task<List<Category>> GetCategoriesAsync()
        {
            await LoadSettingsIfNeeded();

            return _categories;
        }

        public async Task<List<Package>> GetPackagesAsync()
        {
            await LoadSettingsIfNeeded();

            return _packages;
        }

        public async Task<List<PreDb>> GetPreDbsAsync()
        {
            await LoadSettingsIfNeeded();

            return _preDbs;
        }

        public async Task<List<Section>> GetSectionsAsync()
        {
            await LoadSettingsIfNeeded();

            return _sections;
        }

        public async Task<List<Site>> GetSitesAsync()
        {
            await LoadSettingsIfNeeded();

            return _sites;
        }

        public async Task<List<Word>> GetWordsAsync()
        {
            await LoadSettingsIfNeeded();

            return _words;
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

                _categories = dataContract.Categories.Select(c => ContractFactory.GetCategory(c)).ToList();

                _words = dataContract.Words.Select(w => ContractFactory.GetWord(w)).ToList();

                _packages = dataContract.Packages.Select(p => ContractFactory.GetPackage(p)).ToList();

                _sections = dataContract.Sections.Select(s => ContractFactory.GetSection(s)).ToList();

                _complexWords = dataContract.ComplexWords.Select(c => ContractFactory.GetComplexWord(c)).ToList();

                _sites = dataContract.Sites.Select(s => ContractFactory.GetSite(s, _packages)).ToList();

                _preDbs = dataContract.PreDbs.Select(p => ContractFactory.GetPreDb(p)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task LoadSettingsIfNeeded()
        {
            if (_categories == null || _sections == null || _words == null)
                await LoadCacheAsync(_appFile);
        }
    }
}