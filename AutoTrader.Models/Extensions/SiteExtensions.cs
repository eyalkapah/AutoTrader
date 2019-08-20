using AutoTrader.Models.Entities;
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
    }
}