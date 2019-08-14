using AutoTrader.Models.Enums;
using AutoTrader.Models.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class Race
    {
        public ConcurrentDictionary<string, SiteDismiss> DisqualifiedSites { get; }
        public ConcurrentDictionary<string, Participant> Participants { get; }
        public ConcurrentDictionary<string, Participant> ParticipantsSourceQueue { get; }
        public ReleaseBase Release { get; set; }
        public Section Section { get; set; }
        public DateTime PublishDateTime { get; set; }
        public RaceStatus Status { get; set; }

        private CancellationTokenSource _cancellationTokenSource = null;
        private readonly List<Site> _allSites;
        private readonly Branch _branch;
        private readonly List<Package> _packages;
        private readonly List<Word> _words;

        public Race(Section section, ReleaseBase release, List<Site> allSites, Branch branch, List<Package> packages, List<Word> words)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            PublishDateTime = DateTime.Now;

            DisqualifiedSites = new ConcurrentDictionary<string, SiteDismiss>();
            Participants = new ConcurrentDictionary<string, Participant>();
            ParticipantsSourceQueue = new ConcurrentDictionary<string, Participant>();

            Section = section;
            Release = release;
            _allSites = allSites;
            _branch = branch;
            _packages = packages;
            _words = words;
            Status = RaceStatus.Active;

            Task.Run(() =>
            {
                Task.Delay(branch.RaceActivityInSeconds);

                CloseRace();
            });
        }

        public Task InitAsync()
        {
            return Task.Run(() =>
            {
                var sites = new List<Site>(_allSites);

                this.FilterNonSectionSites(sites);
                this.FilterOffStatusSites(sites);
                this.FilterAffiliateUploadOnly(sites);
                this.BuildParticipantsQueue(sites);
            });
        }

        public void AddParticipant(Participant participant)
        {
            Participants.TryAdd(participant.Site.Name, participant);

            if (participant.Role == ParticipantRole.Affiliate && Status == RaceStatus.Active)
            {
                ParticipantsSourceQueue.TryAdd(participant.Site.Name, participant);
            }
        }

        public void DismissSite(Site site, DisqualificationType disqualificationType)
        {
            DisqualifiedSites.TryAdd(site.Name, new SiteDismiss(site, disqualificationType));
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
                            RemoveFromSourceQueue(sSite);
                        }

                        this.ValidatePackages(dSite, _packages, _words);

                        if (dSite.ValidationResult.IsValid == false)
                        {
                        }

                        while (sSite.Logins.Download > 0 && dSite.Logins.Upload > 0)
                        {
                            var tradeCalls = Math.Min(sSite.Logins.Download, dSite.Logins.Upload);

                            //Trade(sSite, dSite, tradeCalls, Section.Name);
                            Debug.WriteLine($"[{sSite.Site.Name}] -> [{dSite.Site.Name}]");

                            sSite.ReduceDownload(1);
                            dSite.ReduceUpload(1);
                        }

                        if (sSite.Role == ParticipantRole.Completed || sSite.Role == ParticipantRole.Downloader)
                            RemoveFromSourceQueue(sSite.Site.Name);
                    }
                }
            });
        }

        private void RemoveFromSourceQueue(Participant site)
        {
            var result = ParticipantsSourceQueue.TryRemove(sitename, out Participant participant);

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