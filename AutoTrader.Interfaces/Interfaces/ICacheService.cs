using AutoTrader.Core.Enums;
using AutoTrader.Models.Entities;
using AutoTrader.Models.Entities.Storage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoTrader.Interfaces.Interfaces
{
    public interface ICacheService
    {
        Task<List<Category>> GetCategoriesAsync();

        Task<List<Package>> GetPackagesAsync();

        Task<List<PreDb>> GetPreDbsAsync();

        Task<List<Section>> GetSectionsAsync();

        Task<List<Site>> GetSitesAsync();

        Task<List<Word>> GetWordsAsync();

        Task LoadCacheAsync(AppFile appFile);
    }
}