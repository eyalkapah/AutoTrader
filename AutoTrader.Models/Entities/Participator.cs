using AutoTrader.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class Participator
    {
        public Enrollment Enrollment { get; set; }
        public int TotalLogins { get; set; }
        public int MaxUploadLogins { get; set; }
        public int MaxDownloadLogins { get; set; }
        public ParticipatorRole Role { get; set; }
    }
}