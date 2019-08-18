using System.Threading.Tasks;
using AutoTrader.Core.Enums;
using AutoTrader.Models.Entities;

namespace AutoTrader.Interfaces.Interfaces
{
    public interface IReleaseService
    {
        Task<ReleaseBase> BuildReleaseAsync(string releaseName, CategoryType categoryType, char delimiter);
    }
}