using AutoTrader.Models.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class Race
    {
        public Section Section { get; set; }
        public ReleaseBase Release { get; set; }
        public ConcurrentBag<Site> QualifiedSites { get; private set; }
        public ConcurrentBag<SiteDismiss> DisqualifiedSites { get; private set; }
        public ConcurrentBag<Participant> Participants { get; set; }
        public ConcurrentBag<Participant> Publishers { get; set; }

        public Race(Section section, ReleaseBase release, List<Site> allSites)
        {
            QualifiedSites = new ConcurrentBag<Site>();
            DisqualifiedSites = new ConcurrentBag<SiteDismiss>();
            Publishers = new ConcurrentBag<Participant>();

            allSites.ForEach(site => QualifiedSites.Add(site));
            Section = section;
            Release = release;
        }

        public void DismissSite(Site site, DisqualificationType disqualificationType)
        {
            QualifiedSites.TryTake(out Site removedSite);
            DisqualifiedSites.Add(new SiteDismiss(site, disqualificationType));
        }
    }
}