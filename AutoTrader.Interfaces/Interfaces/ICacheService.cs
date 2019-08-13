using AutoTrader.Core.Enums;
using AutoTrader.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoTrader.Core.Services
{
    public interface ICacheService
    {
        Task<List<Category>> GetCategoriesAsync();

        Task<List<Section>> GetSectionsAsync();

        Task<List<Site>> GetSitesAsync();

        Task<List<Word>> GetWordsAsync();

        Task<List<Branch>> GetBranchesAsync();

        Task<List<PreDb>> GetPreDbsAsync();
    }
}