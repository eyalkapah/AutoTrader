using AutoTrader.Models.Entities;
using AutoTrader.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Extensions
{
    public static class SiteExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="allsites">All sites</param>
        /// <param name="sectionId">section participated</param>
        /// <returns></returns>
        public static List<Enrollment> GetParticipatingEnrolls(this IEnumerable<Site> allsites, ReleaseBase release, string sectionId)
        {
            var race = new Race();

            var sites = allsites.Where(s => s.Status != SiteStatus.Off);

            var qualified = new List<Participator>();
            foreach (var site in sites)
            {
                // No section
                if (!site.Enrollments.Any(e => e.SectionId.Equals(sectionId)))
                {
                    race.DismissedSites.Add(new SiteDismiss(site, DisqualificationType.SectionNotExist));
                    continue;
                }

                // Status off
                if (site.Status == SiteStatus.Off)
                {
                    race.DismissedSites.Add(new SiteDismiss(site, DisqualificationType.SiteStatusOff));
                    continue;
                }
            }

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
                    {
                        race.Affiliates.Add(site, );
                        continue;
                    }
                }
            }
        }
    }