using System.Threading.Tasks;
using AutoTrader.Models.Entities;

namespace AutoTrader.Interfaces.Interfaces
{
    public interface IBranchService
    {
        Task<Branch> GetBranchBySectionIdAsync(string sectionId);
    }
}