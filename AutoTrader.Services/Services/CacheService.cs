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

        private Data _data;

        public List<Category> Categories => _data.Categories;
        public List<ComplexWord> ComplexWords => _data.ComplexWords;
        public List<Package> Packages => _data.Packages;
        public List<PreDb> PreDbs => _data.PreDbs;

        public List<Section> Sections => _data.Sections;

        public List<Site> Sites => _data.Sites;

        public List<Word> Words => _data.Words;

        public CacheService()
        {
            _data = new Data();
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

                _data = new Data
                {
                    Categories = dataContract.Categories.Select(c => ContractFactory.GetCategory(c)).ToList() ?? new List<Category>(),
                    Words = dataContract.Words.Select(w => ContractFactory.GetWord(w)).ToList() ?? new List<Word>(),
                    Packages = dataContract.Packages.Select(p => ContractFactory.GetPackage(p)).ToList() ?? new List<Package>(),
                    Sections = dataContract.Sections.Select(s => ContractFactory.GetSection(s)).ToList() ?? new List<Section>(),
                    ComplexWords = dataContract.ComplexWords.Select(c => ContractFactory.GetComplexWord(c)).ToList() ?? new List<ComplexWord>(),
                    Sites = dataContract.Sites.Select(s => ContractFactory.GetSite(s, Packages)).ToList() ?? new List<Site>(),
                    PreDbs = dataContract.PreDbs.Select(p => ContractFactory.GetPreDb(p)).ToList() ?? new List<PreDb>()
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SaveData()
        {
        }
    }
}