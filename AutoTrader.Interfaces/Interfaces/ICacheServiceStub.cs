using AutoTrader.Core.Enums;
using AutoTrader.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoTrader.Core.Services
{
    public interface ICacheService
    {
        List<Category> Categories { get; set; }
        List<Section> Sections { get; set; }

        Section GetSection(string name);

        Task ReadCache();
    }
}