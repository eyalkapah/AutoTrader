using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities.Storage
{
    public class AppFile
    {
        public string FileName { get; set; }
        public string Folder { get; set; }

        public string FullPath => Path.Combine(Folder, FileName);

        public string InstallationDefaultFileName { get; set; }
        public string InstallationDefaultFolder { get; set; }
        public string InstallationFullPath => Path.Combine(InstallationDefaultFolder, InstallationDefaultFileName);
    }
}