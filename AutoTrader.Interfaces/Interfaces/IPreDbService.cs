using System.Collections.Generic;
using System.Threading.Tasks;
using AutoTrader.Models.Entities;

namespace AutoTrader.Interfaces.Interfaces
{
    public interface IPreDbService
    {
        PreDb GetPreDb(string channel, string bot);

        List<PreDb> GetPreDbs();
    }
}