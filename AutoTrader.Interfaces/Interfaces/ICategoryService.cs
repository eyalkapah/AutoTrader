using System.Threading.Tasks;
using AutoTrader.Core.Enums;

namespace AutoTrader.Interfaces.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryType> GetCategoryType(string sectionName);
    }
}