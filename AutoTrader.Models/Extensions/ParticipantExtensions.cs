using AutoTrader.Models.Entities;
using AutoTrader.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Extensions
{
    public static class ParticipantExtensions
    {
        public static Participant GetTopRatedSite(this IEnumerable<Participant> participants, IEnumerable<ParticipantRole> roles)
        {
            return GetTopRatedSites(participants, roles).FirstOrDefault();
        }

        public static IEnumerable<Participant> GetTopRatedSites(this IEnumerable<Participant> participants, IEnumerable<ParticipantRole> roles)
        {
            return participants.Where(p => roles.Contains(p.Role)).OrderByDescending(p => p.Rank);
        }

        public static IEnumerable<Participant> GetDestinationSites(this IEnumerable<Participant> participants)
        {
            return participants.GetTopRatedSites(new[] { ParticipantRole.UploaderAndDownloader, ParticipantRole.Downloader });
        }
    }
}