using System.Collections.Generic;
using System.Threading.Tasks;
using AutoTrader.Models.Entities;

namespace AutoTrader.Interfaces.Interfaces
{
    public interface IPackageService
    {
        List<Package> GetPackages();

        bool IsPackageValid(string packageId, string text);
    }
}