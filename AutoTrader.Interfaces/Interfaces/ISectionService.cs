using System.Threading.Tasks;
using AutoTrader.Models.Entities;

namespace AutoTrader.Interfaces.Interfaces
{
    public interface ISectionService
    {
        Task<Section> GetSectionAsync(string name);
    }
}