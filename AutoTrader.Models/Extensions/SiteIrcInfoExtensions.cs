using AutoTrader.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Extensions
{
    public static class SiteIrcInfoExtensions
    {
        public static bool IsIrcInfoSame(this SiteIrcInfo ircInfo, string channel, string bot)
        {
            return ircInfo.Channel.TrimStart('#').Equals(channel.TrimStart('#')) && ircInfo.Bot.Equals(bot);
        }
    }
}