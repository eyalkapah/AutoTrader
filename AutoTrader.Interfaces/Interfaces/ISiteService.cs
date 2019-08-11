using System.Collections.Generic;
using System.Threading.Tasks;
using AutoTrader.Models.Entities;

namespace AutoTrader.Services.Services
{
    public interface ISiteService
    {
        Task<List<Enrollment>> GetParticipatingSites(string sectionId);

        Task<List<Site>> GetSitesAsync(string sectionId);
    }
}