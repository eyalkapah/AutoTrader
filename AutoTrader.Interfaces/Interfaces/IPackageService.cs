using System.Collections.Generic;
using System.Threading.Tasks;
using AutoTrader.Models.Entities;

namespace AutoTrader.Interfaces.Interfaces
{
    public interface IPackageService
    {
        Task<List<Package>> GetPackagesAsync();

        Task<bool> IsPackageValidAsync(string packageId, string text);
    }
}