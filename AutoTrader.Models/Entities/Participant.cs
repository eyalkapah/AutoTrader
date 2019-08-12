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
        public ParticipatorRole Role { get; set; }

        public Participant()
        {
            Role = ParticipatorRole.Regular;
        }
    }
}