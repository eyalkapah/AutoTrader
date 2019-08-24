using System.Collections.Generic;
using System.Threading.Tasks;
using AutoTrader.Models.Entities;

namespace AutoTrader.Interfaces.Interfaces
{
    public interface ISectionService
    {
        Section GetSection(string name);

        IEnumerable<Section> GetSections();
    }
}