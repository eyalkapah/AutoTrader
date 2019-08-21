using AutoTrader.Models.Entities;
using AutoTrader.Models.Enums;
using AutoTrader.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Extensions
{
    public static class RaceExtensions
    {
        public static Participant BuildParticipant(this Race race, Site site)
        {
            var enrollment = site.Enrollments.Single(e => e.SectionId.Equals(race.Section.Id));
            var isAffiliate = enrollment.Affils.Contains(race.Release.Group);

            return site.ConvertToParticipant(enrollment, isAffiliate);
        }

        public static void BuildParticipantsQueue(this Race race, IEnumerable<Site> sites)
        {
            IterateParallel(race, sites, BuildParticipantsQueueAction);
        }

        public static void FilterAffiliateUploadOnly(this Race race, IEnumerable<Site> sites)
        {
            IterateParallel(race, sites, FilterAffiliateUploadOnlyAction);
        }

        public static void FilterNonSectionSites(this Race race, IEnumerable<Site> sites)
        {
            IterateParallel(race, sites, FilterNonSectionSitesAction);
        }

        public static void FilterOffStatusSites(this Race race, IEnumerable<Site> sites)
        {
            IterateParallel(race, sites, FilterOffStatusAction);
        }

        // Gets
        // 1. sSide != dSite && (UploaderAndDownloder || Downloader) && BubbleLevelOK
        public static Participant GetDestinationSite(this Race race, Participant sSite, int bubbleLevel)
        {
            if (race.Participants.Any())
            {
                var dSites = race.Participants.Values;

                return dSites.FirstOrDefault(d => d.Site.Id != sSite.Site.Id
                        && (d.Role == ParticipantRole.UploaderAndDownloader
                        || d.Role == ParticipantRole.Downloader
                        && sSite.Rank <= d.Rank + bubbleLevel));
            }
            return null;
        }

        // Gets either:
        // 1.Affiliate
        // 2.UploaderAndDownloader
        // 3.Uploader
        public static Participant GetSourceSite(this Race race)
        {
            if (race.Participants.Any())
            {
                var affiliate = race.ParticipantsSourceQueue.Values.GetTopRatedSite(new[] { ParticipantRole.Affiliate });

                if (affiliate != null)
                    return affiliate;

                return race.ParticipantsSourceQueue.Values.GetTopRatedSite(new[] { ParticipantRole.UploaderAndDownloader, ParticipantRole.Uploader });
            }

            return null;
        }

        public static void ValidatePackages(this Race race, Participant dSite, List<Package> packages, List<Word> words)
        {
            if (dSite.ValidationResult != null)
                return;

            Parallel.ForEach(dSite.Enrollment.PackagesIds, pId =>
            {
                var package = packages.Single(p => p.Id == pId);

                if (!package.IsPackageValid(words, race.Release.Name))
                {
                    dSite.ValidationResult = new PackageValidationResult
                    {
                        IsValid = false,
                        PackageId = package.Id
                    };
                }
            });

            if (dSite.ValidationResult != null && dSite.ValidationResult.IsValid != false)
            {
                dSite.ValidationResult = new PackageValidationResult
                {
                    IsValid = true
                };
            }
        }

        private static void BuildParticipantsQueueAction(this Race race, Site site)
        {
            race.AddParticipant(BuildParticipant(race, site));
        }

        private static void FilterAffiliateUploadOnlyAction(Race race, Site site)
        {
            var enrollment = site.Enrollments.Single(e => e.SectionId.Equals(race.Section.Id));

            // Affiliate upload only
            if (enrollment.Affils.Contains(race.Release.Group))
            {
                // Site upload only
                if (site.Status == SiteStatus.UploadOnly)
                {
                    race.DismissSite(site, DisqualificationType.SiteUploadOnlyAffiliate);
                }

                // Section upload only
                if (site.Status == SiteStatus.Mixed && enrollment.Status == EnrollmentStatus.UploadOnly)
                {
                    race.DismissSite(site, DisqualificationType.SectionUploadOnlyAffiliate);
                }
            }
        }

        private static void FilterNonSectionSitesAction(this Race race, Site site)
        {
            if (!site.Enrollments.Any(e => e.SectionId.Equals(race.Section.Id)))
            {
                race.DismissSite(site, DisqualificationType.SectionNotExist);
            }
        }

        private static void FilterOffStatusAction(this Race race, Site site)
        {
            if (site.Status == SiteStatus.Off)
            {
                race.DismissSite(site, DisqualificationType.SiteStatusOff);
            }
            else
            {
                var enrollment = site.Enrollments.Single(e => e.SectionId.Equals(race.Section.Id));

                if (enrollment.Status == EnrollmentStatus.Off)
                {
                    race.DismissSite(site, DisqualificationType.SectionStatusOff);
                }
            }
        }

        private static void IterateParallel(Race race, IEnumerable<Site> sites, Action<Race, Site> parallelAction)
        {
            Parallel.ForEach(sites, site =>
            {
                parallelAction(race, site);
            });
        }
    }
}