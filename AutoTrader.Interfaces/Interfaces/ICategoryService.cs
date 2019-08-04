using System.Threading.Tasks;
using AutoTrader.Core.Enums;
using AutoTrader.Models.Entities;

namespace AutoTrader.Interfaces.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryType> GetCategoryType(string sectionName);

        Task<Category> GetCategoryAsync(string sectionName);
    }
}