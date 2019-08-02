using AutoTrader.Core.Services;
using AutoTrader.Interfaces.Interfaces;
using AutoTrader.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Services.Services
{
    public class DataProviderService : IDataProvider
    {
        public async Task<List<CategoryContract>> GetCategoriesAsync()
        {
            try
            {
                var categories = await DataProvider.GetCategoriesAsync();
                return categories;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}