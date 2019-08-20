using AutoTrader.Core.Enums;
using AutoTrader.Models.Entities;
using AutoTrader.Models.Entities.Storage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoTrader.Interfaces.Interfaces
{
    public interface ICacheService
    {
        List<Category> Categories { get; }

        List<ComplexWord> ComplexWords { get; }
        List<Package> Packages { get; }

        List<PreDb> PreDbs { get; }

        List<Section> Sections { get; }

        List<Site> Sites { get; }

        List<Word> Words { get; }

        Task LoadCacheAsync(AppFile appFile);

        Task SaveDataAsync();
    }
}