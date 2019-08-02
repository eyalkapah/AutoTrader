using System.Threading.Tasks;
using AutoTrader.Core.Enums;

namespace AutoTrader.Core.Services
{
    public interface ICategoryService
    {
        Task<ReleaseCategoryType> GetCategoryType(string sectionName);
    }
}