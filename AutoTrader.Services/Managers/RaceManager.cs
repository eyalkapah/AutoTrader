using AutoTrader.Models.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Services.Managers
{
    public class RaceManager
    {
        private readonly ConcurrentDictionary<string, Race> _races;
        private readonly ConcurrentDictionary<string, Race> _recentRaces;

        public RaceManager()
        {
            _races = new ConcurrentDictionary<string, Race>();
            _recentRaces = new ConcurrentDictionary<string, Race>();
        }

        public void AddRace(Race race)
        {
            _races.TryAdd(race.Release.Name, race);
            _recentRaces.TryAdd(race.Release.Name, race);

            race.RaceClosed += OnRaceClosed;
        }

        public Race GetRace(string releaseName)
        {
            if (_recentRaces.TryGetValue(releaseName, out Race race))
                return race;

            if (_races.TryGetValue(releaseName, out race))
                return race;

            return null;
        }

        private void OnRaceClosed(object sender, string releaseName)
        {
            _recentRaces.TryRemove(releaseName, out Race race);

            race.RaceClosed -= OnRaceClosed;
        }
    }
}