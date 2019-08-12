using AutoTrader.Models.Entities;
using AutoTrader.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Extensions
{
    public static class RaceExtensions
    {
        public static void FilterNonSectionSites(this Race race)
        {
            IterateParallel(race, FilterNonSectionSitesAction);
        }

        public static void FilterOffStatusSites(this Race race)
        {
            IterateParallel(race, FilterOffStatusAction);
        }

        public static void FilterAffiliateUploadOnly(this Race race)
        {
            IterateParallel(race, FilterAffiliateUploadOnlyAction);
        }

        public static void BuildParticipants(this Race race)
        {
            IterateParallel(race, BuildParticipantsAction);
        }

        public static Participant PopSourceSite(this Race race)
        {
            var affiliate = race.Participants.GetTopRatedAffiliate();

            if (affiliate != null)
                return affiliate;

            // TODO: Get next publisher
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

                if (enrollment.Status == SiteStatus.Off)
                {
                    race.DismissSite(site, DisqualificationType.SectionStatusOff);
                }
            }
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
                if (site.Status == SiteStatus.Mixed && enrollment.Status == SiteStatus.UploadOnly)
                {
                    race.DismissSite(site, DisqualificationType.SectionUploadOnlyAffiliate);
                }
            }
        }

        private static void BuildParticipantsAction(this Race race, Site site)
        {
            var enrollment = site.Enrollments.Single(e => e.SectionId.Equals(race.Section.Id));
            var isAffiliate = enrollment.Affils.Contains(race.Release.Group);

            race.Participants.Add(new Participant
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
                Role = isAffiliate ? ParticipatorRole.Affiliate : ParticipatorRole.Regular
            });
        }

        private static void IterateParallel(Race race, Action<Race, Site> parallelAction)
        {
            Parallel.ForEach(race.QualifiedSites, site =>
            {
                parallelAction(race, site);
            });
        }
    }
}