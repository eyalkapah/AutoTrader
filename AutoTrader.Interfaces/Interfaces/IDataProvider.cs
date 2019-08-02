using AutoTrader.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoTrader.Interfaces.Interfaces
{
    public interface IDataProvider
    {
        Task<List<CategoryContract>> GetCategoriesAsync();
    }
}