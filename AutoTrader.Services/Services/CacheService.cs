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
                    _data = await Task.Run(() => JsonHelper.DeserializeJson<Data>(appFile.FullPath));
                }
                else
                {
                    if (!File.Exists(appFile.InstallationFullPath))
                        throw new FileNotFoundException($"Couldn't find default file: {appFile.InstallationFullPath}");

                    dataContract = await Task.Run(() => JsonHelper.DeserializeJson<DataContract>(appFile.InstallationFullPath));

                    await Task.Run(() => JsonHelper.SerializeJson(dataContract, appFile.FullPath));

                    var categories = dataContract.Categories.Select(c => ContractFactory.GetCategory(c)).ToList() ?? new List<Category>();
                    var words = dataContract.Words.Select(w => ContractFactory.GetWord(w)).ToList() ?? new List<Word>();
                    var packages = dataContract.Packages.Select(p => ContractFactory.GetPackage(p)).ToList() ?? new List<Package>();
                    var sections = dataContract.Sections.Select(s => ContractFactory.GetSection(s)).ToList() ?? new List<Section>();
                    var complexWords = dataContract.ComplexWords.Select(c => ContractFactory.GetComplexWord(c)).ToList() ?? new List<ComplexWord>();
                    var sites = dataContract.Sites.Select(s => ContractFactory.GetSite(s, packages)).ToList() ?? new List<Site>();
                    var preDbs = dataContract.PreDbs.Select(p => ContractFactory.GetPreDb(p)).ToList() ?? new List<PreDb>();

                    _data = new Data
                    {
                        Categories = categories,
                        Words = words,
                        Packages = packages,
                        Sections = sections,
                        ComplexWords = complexWords,
                        Sites = sites,
                        PreDbs = preDbs
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SaveDataAsync()
        {
            try
            {
                if (File.Exists(_appFile.FullPath))
                {
                    File.Move(_appFile.FullPath, $"{_appFile.FullPath}-{DateTime.Now.Ticks}.bak");
                }

                await Task.Run(() => JsonHelper.SerializeJson(_data, _appFile.FullPath));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}