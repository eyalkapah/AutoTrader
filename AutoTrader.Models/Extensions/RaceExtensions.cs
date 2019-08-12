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
            FilterInParallel(race, site => !site.Enrollments.Any(e => e.SectionId.Equals(race.Section.Id)), DisqualificationType.SectionNotExist);
        }

        public static void FilterOffStatusSites(this Race race)
        {
            FilterInParallel(race, site => site.Status == SiteStatus.Off, DisqualificationType.SiteStatusOff);
        }

        public static void FilterOffStatusEnrollments(this Race race)
        {
            Parallel.ForEach(race.QualifiedSites.SelectMany(s => s.Enrollments.Where(e => e.SectionId.Equals(race.Section.Id))), (e, s) =>
            {
                if (e.Status == SiteStatus.Off)
                {
                    race.DismissSite(s, DisqualificationType.SectionStatusOff);
                }
            });
        }

        private static void FilterInParallel<T>(Race race, Func<Site, bool> predicate, DisqualificationType disqualification)
        {
            Parallel.ForEach(race.QualifiedSites, site =>
            {
                if (predicate(site))
                {
                    race.DismissSite(site, disqualification);
                }
            });
        }

        public static Race GetParticipatingEnrolls(this Race race, IEnumerable<Site> allsites)
        {
            var sites = allsites.Where(s => s.Status != SiteStatus.Off);

            sites = sites.Where(s => !race.DismissedSites.Any(d => d.Site.Id == s.Id));

            var enrolls = sites.SelectMany(s => s.Enrollments.Where(e => e.SectionId.Equals(sectionId) && e.Status != SiteStatus.Off));

            foreach (var site in sites)
            {
                var enrollment = site.Enrollments.First(e => e.SectionId.Equals(sectionId));

                // Section status off
                if (enrollment.Status == SiteStatus.Off)
                {
                    race.DismissedSites.Add(new SiteDismiss(site, DisqualificationType.SectionStatusOff));
                    continue;
                }

                // Affiliate upload only
                if (enrollment.Affils.Contains(release.Group))
                {
                    // Site upload only
                    if (site.Status == SiteStatus.UploadOnly)
                    {
                        race.DismissedSites.Add(new SiteDismiss(site, DisqualificationType.SiteUploadOnlyAffiliate));
                        continue;
                    }

                    // Section upload only
                    if (site.Status == SiteStatus.Mixed && enrollment.Status == SiteStatus.UploadOnly)
                    {
                        race.DismissedSites.Add(new SiteDismiss(site, DisqualificationType.SectionUploadOnlyAffiliate));
                        continue;
                    }

                    // Affiliate
                    {
                        race.Participants.Add(new Participant
                        {
                            Site = site,
                            Enrollment = enrollment,
                            Logins = new Logins
                            {
                                Total = site.Logins.Total,
                                Upload = site.Logins.Upload,
                                Download = site.Logins.Download
                            },
                            Role = ParticipatorRole.Affiliate
                        });
                        continue;
                    }
                }
            }
        }
    }
}