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
            return participants.Where(p => roles.Contains(p.Role)).OrderByDescending(p => p.Rank).FirstOrDefault();
        }
    }
}