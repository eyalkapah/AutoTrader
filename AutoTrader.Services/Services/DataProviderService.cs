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
    public class DataProviderService : IDataProviderService
    {
        public Task<SettingsContract> GetSettingsAsync()
        {
            return DataProvider.GetSettings();
        }
    }
}