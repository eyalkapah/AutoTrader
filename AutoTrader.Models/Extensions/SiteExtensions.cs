using AutoTrader.Models.Entities;
using AutoTrader.Models.Enums;
using AutoTrader.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Extensions
{
    public static class SiteExtensions
    {
        public static bool IsIrcInfo(this Site site, string channel, string bot)
        {
            foreach (var ircInfo in site.IrcInfo)
            {
                if (ircInfo.Channel.TrimStart('#').Equals(channel.TrimStart('#')) && ircInfo.Bot.Equals(bot))
                    return true;
            }

            return false;
        }

        public static Participant ConvertToParticipant(this Site site, Enrollment enrollment, bool isAffiliate)
        {
            return new Participant
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
                Role = HelperMethods.GetParticipantRole(site, enrollment, isAffiliate)
            };
        }
    }
}