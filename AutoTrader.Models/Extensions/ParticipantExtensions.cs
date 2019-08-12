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
        public static Participant GetTopRatedParticipant(this IEnumerable<Participant> participants, ParticipatorRole role)
        {
            return participants.Where(p => p.Role == role).OrderByDescending(p => p.Rank).FirstOrDefault();
        }
    }
}