using AutoTrader.Interfaces.Interfaces;
using AutoTrader.Models.Entities.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.ApplicationModel;
using System.IO;

namespace AutoTrader.Windows.Service
{
    public class AppLifeCycleService : IAppLifeCycleService
    {
        private readonly ICacheService _cacheService;

        public AppLifeCycleService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public void InitFromCache()
        {
            _cacheService.LoadCacheAsync(new AppFile
            {
                Folder = ApplicationData.Current.LocalFolder.Path,
                InstallationDefaultFolder = Path.Combine(Package.Current.InstalledLocation.Path, "Assets", "SampleData"),
                FileName = "Data.json"
            });
        }
    }
}