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
        public static Participant GetTopRatedAffiliate(this IEnumerable<Participant> participants)
        {
            return participants.Where(p => p.Role == ParticipatorRole.Affiliate).OrderByDescending(p => p.Rank).FirstOrDefault();
        }
    }
}