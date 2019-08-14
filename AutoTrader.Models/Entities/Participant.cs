using AutoTrader.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class Participant
    {
        public Site Site { get; set; }
        public Enrollment Enrollment { get; set; }
        public Logins Logins { get; set; }
        public int Rank { get; internal set; }
        public ParticipantRole Role { get; set; }
        public PackageValidationResult ValidationResult { get; internal set; }

        public Participant()
        {
            Role = ParticipantRole.UploaderAndDownloader;
        }

        public void ReduceDownload(int count)
        {
            Logins.ReduceDownload(count);

            if (Logins.Total == 0)
                Role = ParticipantRole.Completed;
            else if (Logins.Download == 0)
            {
                if (Role == ParticipantRole.Downloader)
                    Role = ParticipantRole.Completed;
                else if (Role == ParticipantRole.UploaderAndDownloader)
                    Role = ParticipantRole.Uploader;
            }
        }

        public void ReduceUpload(int count)
        {
            Logins.ReduceUpload(count);

            if (Logins.Total == 0)
                Role = ParticipantRole.Completed;
            else if (Logins.Upload == 0)
            {
                if (Role == ParticipantRole.Uploader)
                    Role = ParticipantRole.Completed;
                else if (Role == ParticipantRole.UploaderAndDownloader)
                    Role = ParticipantRole.Downloader;
            }
        }
    }
}