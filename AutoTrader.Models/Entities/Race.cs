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
        private readonly List<Site> _allSites;
        private readonly Branch _branch;
        private readonly List<Package> _packages;
        private readonly List<Word> _words;
        private CancellationTokenSource _cancellationTokenSource = null;

        public event EventHandler<string> RaceClosed = delegate { };

        public ConcurrentDictionary<string, Participant> Participants { get; }
        public ConcurrentDictionary<string, Participant> ParticipantsSourceQueue { get; }
        public DateTime PublishDateTime { get; set; }
        public ReleaseBase Release { get; set; }
        public Section Section { get; set; }
        public RaceStatus Status { get; set; }
        private ConcurrentDictionary<string, SiteDismiss> _disqualifiedSites { get; }

        public Race(Section section, ReleaseBase release, List<Site> allSites, Branch branch, List<Package> packages, List<Word> words)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            PublishDateTime = DateTime.Now;

            _disqualifiedSites = new ConcurrentDictionary<string, SiteDismiss>();
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

        public void AddParticipant(Participant participant)
        {
            Participants.TryAdd(participant.Site.Name, participant);

            if (participant.Role == ParticipantRole.Affiliate && Status == RaceStatus.Active)
            {
                ParticipantsSourceQueue.TryAdd(participant.Site.Name, participant);
            }
        }

        public void CloseRace()
        {
            Status = RaceStatus.Completed;

            _cancellationTokenSource.Cancel();

            RaceClosed(this, Release.Name);
        }

        public void DismissSite(Site site, DisqualificationType disqualificationType)
        {
            _disqualifiedSites.TryAdd(site.Name, new SiteDismiss(site, disqualificationType));
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

        public void StartRace(Site site)
        {
            if (site != null)
            {
                AddParticipant(this.BuildParticipant(site));
            }

            if (Status != RaceStatus.NotStarted)
                return;
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
                        RemoveFromQueueAndUpdateRole(sSite);
                    }
                    else
                    {
                        this.ValidatePackages(dSite, _packages, _words);

                        if (dSite.ValidationResult.IsValid == false)
                        {
                            dSite.Role = ParticipantRole.Aborted;
                        }
                        else
                        {
                            while (sSite.Logins.Download > 0 && dSite.Logins.Upload > 0)
                            {
                                var tradeCalls = Math.Min(sSite.Logins.Download, dSite.Logins.Upload);

                                //Trade(sSite, dSite, tradeCalls, Section.Name);
                                Debug.WriteLine($"[{sSite.Site.Name}] -> [{dSite.Site.Name}]");

                                sSite.ReduceDownload(1);
                                dSite.ReduceUpload(1);
                            }

                            if (sSite.Role == ParticipantRole.Completed || sSite.Role == ParticipantRole.Downloader)
                                RemoveFromQueueAndUpdateRole(sSite);
                        }
                    }
                }
            }
        }

        private void RemoveFromQueueAndUpdateRole(Participant participant)
        {
            var result = ParticipantsSourceQueue.TryRemove(participant.Site.Name, out Participant remove);

            if (!result)
                throw new InvalidOperationException("Failed to remove site from queue");

            if (participant.Role == ParticipantRole.UploaderAndDownloader)
                participant.Role = ParticipantRole.Downloader;
        }
    }
}