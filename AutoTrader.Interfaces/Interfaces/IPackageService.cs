using System.Threading.Tasks;
using AutoTrader.Models.Entities;

namespace AutoTrader.Interfaces.Interfaces
{
    public interface IPackageService
    {
        Task<bool> IsPackageValidAsync(Package package, string text);
    }
}