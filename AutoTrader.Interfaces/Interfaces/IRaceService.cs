using System.Collections.Generic;
using System.Threading.Tasks;
using AutoTrader.Models.Entities;

namespace AutoTrader.Interfaces.Interfaces
{
    public interface IRaceService
    {
        List<Race> Races { get; set; }

        Task<Race> BuildRaceAsync(string releaseName, Section section, IrcPublisher ircPublisher);

        Task RaceAsync(string releaseName, string sectionName, IrcPublisher ircPublisher);
    }
}