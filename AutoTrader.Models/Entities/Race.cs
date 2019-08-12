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
        public ConcurrentBag<SiteDismiss> DisqualifiedSites { get; }
        public ConcurrentBag<Participant> Participants { get; }
        public ConcurrentBag<Participant> ParticipantsQueue { get; }
        public ConcurrentBag<Site> QualifiedSites { get; }
        public ReleaseBase Release { get; set; }
        public Section Section { get; set; }

        public Race(Section section, ReleaseBase release, List<Site> allSites)
        {
            QualifiedSites = new ConcurrentBag<Site>();
            DisqualifiedSites = new ConcurrentBag<SiteDismiss>();
            Participants = new ConcurrentBag<Participant>();
            ParticipantsQueue = new ConcurrentBag<Participant>();

            allSites.ForEach(site => QualifiedSites.Add(site));
            Section = section;
            Release = release;
        }

        public void AddParticipant(Participant participant)
        {
            Participants.Add(participant);

            if (participant.Role == ParticipatorRole.Affiliate)
                ParticipantsQueue.Add(participant);
        }

        public void DismissSite(Site site, DisqualificationType disqualificationType)
        {
            QualifiedSites.TryTake(out Site removedSite);
            DisqualifiedSites.Add(new SiteDismiss(site, disqualificationType));
        }
    }
}