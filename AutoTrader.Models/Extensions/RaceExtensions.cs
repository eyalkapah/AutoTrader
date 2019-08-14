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

        public static PackageValidationResult ValidatePackages(this Race race, Participant dSite, List<Package> packages, List<Word> words)
        {
            PackageValidationResult validationResult = null;

            Parallel.ForEach(dSite.Enrollment.PackagesIds, pId =>
            {
                var package = packages.Single(p => p.Id == pId);

                if (!package.IsPackageValid(words, race.Release.Name))
                {
                    validationResult = new PackageValidationResult
                    {
                        IsValid = false,
                        PackageId = package.Id
                    };
                }
            });

            return validationResult;
        }

        public static Participant GetSourceSite(this Race race)
        {
            if (race.Participants.Any())
            {
                var affiliate = race.ParticipantsQueue.Values.GetTopRatedSite(new[] { ParticipantRole.Affiliate });

                if (affiliate != null)
                    return affiliate;

                return race.ParticipantsQueue.Values.GetTopRatedSite(new[] { ParticipantRole.Regular, ParticipantRole.Uploader });
            }

            return null;
        }

        public static Participant GetDestinationSite(this Race race, Participant sSite, int bubbleLevel)
        {
            if (race.Participants.Any())
            {
                var dSites = race.Participants.Values;

                return dSites.FirstOrDefault(d => d.Site.Id != sSite.Site.Id
                        && (d.Role == ParticipantRole.Affiliate || sSite.Rank <= d.Rank + bubbleLevel));
            }
            return null;
        }

        private static void BuildParticipantsQueueAction(this Race race, Site site)
        {
            var enrollment = site.Enrollments.Single(e => e.SectionId.Equals(race.Section.Id));
            var isAffiliate = enrollment.Affils.Contains(race.Release.Group);

            race.AddParticipant(new Participant
            {
                Site = site,
                Enrollment = enrollment,
                Logins = new Logins
                {
                    Total = site.Logins.Total,
                    Download = site.Logins.Download,
                    Upload = site.Logins.Upload
                },
                Rank = site.Rank,
                Role = GetParticipantRole(site, enrollment, isAffiliate)
            });
        }

        private static ParticipantRole GetParticipantRole(Site site, Enrollment enrollment, bool isAffiliate)
        {
            if (isAffiliate)
                return ParticipantRole.Affiliate;

            switch (site.Status)
            {
                case SiteStatus.On:
                    return ParticipantRole.Regular;

                case SiteStatus.UploadOnly:
                    return ParticipantRole.Uploader;

                case SiteStatus.DownloadOnly:
                    return ParticipantRole.Downloader;

                case SiteStatus.Off:
                    break;

                case SiteStatus.Mixed:
                    if (enrollment.Status == EnrollmentStatus.DownloadOnly)
                        return ParticipantRole.Downloader;
                    if (enrollment.Status == EnrollmentStatus.UploadOnly)
                        return ParticipantRole.Uploader;
                    if (enrollment.Status == EnrollmentStatus.On)
                        return ParticipantRole.Regular;

                    throw new BadParticipantRoleException(site.Name);
            }

            throw new UnknownSiteStatusException(site.Name);
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