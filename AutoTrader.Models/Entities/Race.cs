using AutoTrader.Models.Enums;
using AutoTrader.Models.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public DateTime PublishDateTime { get; set; }
        public RaceStatus Status { get; set; }

        private CancellationTokenSource _cancellationTokenSource = null;
        private readonly Branch _branch;

        public Race(Section section, ReleaseBase release, List<Site> allSites, Branch branch)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            PublishDateTime = DateTime.Now;

            QualifiedSites = new ConcurrentBag<Site>();
            DisqualifiedSites = new ConcurrentBag<SiteDismiss>();
            Participants = new ConcurrentBag<Participant>();
            ParticipantsQueue = new ConcurrentBag<Participant>();

            allSites.ForEach(site => QualifiedSites.Add(site));
            Section = section;
            Release = release;
            _branch = branch;
            Status = RaceStatus.Active;

            Task.Run(() =>
            {
                Task.Delay(branch.RaceActivityInSeconds);

                CloseRace();
            });
        }

        public void AddParticipant(Participant participant)
        {
            Participants.Add(participant);

            if (participant.Role == ParticipantRole.Affiliate && Status == RaceStatus.Active)
            {
                ParticipantsQueue.Add(participant);
            }
        }

        public void DismissSite(Site site, DisqualificationType disqualificationType)
        {
            QualifiedSites.TryTake(out Site removedSite);
            DisqualifiedSites.Add(new SiteDismiss(site, disqualificationType));
        }

        public Task RaceAsync()
        {
            return Task.Run(() =>
            {
                while (true)
                {
                    if (_cancellationTokenSource.Token.IsCancellationRequested)
                        break;

                    var sSite = this.GetSourceSite();

                    if (sSite != null)
                    {
                        var dSite = this.GetDestinationSite(sSite, _branch.BubbleLevel);

                        if (dSite == null)
                        {
                            RemoveSiteFromQueue(sSite);
                        }
                    }
                }
            });
        }

        private void RemoveSiteFromQueue(Participant sSite)
        {
            var result = ParticipantsQueue.TryTake(out sSite);

            if (!result)
                throw new InvalidOperationException("Failed to remove site from queue");
        }

        public void CloseRace()
        {
            Status = RaceStatus.Completed;

            _cancellationTokenSource.Cancel();
        }
    }
}