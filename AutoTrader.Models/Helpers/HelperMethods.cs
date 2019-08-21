using AutoTrader.Models.Entities;
using AutoTrader.Models.Enums;
using AutoTrader.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Helpers
{
    public class HelperMethods
    {
        public static ParticipantRole GetParticipantRole(Site site, Enrollment enrollment, bool isAffiliate)
        {
            if (isAffiliate)
                return ParticipantRole.Affiliate;

            switch (site.Status)
            {
                case SiteStatus.On:
                    return ParticipantRole.UploaderAndDownloader;

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
                        return ParticipantRole.UploaderAndDownloader;

                    throw new BadParticipantRoleException(site.Name);
            }

            throw new UnknownSiteStatusException(site.Name);
        }
    }
}