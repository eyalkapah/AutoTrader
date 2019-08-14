using AutoTrader.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class Enrollment
    {
        public string Id { get; set; }
        public string SectionId { get; set; }
        public List<string> Affils { get; set; }
        public EnrollmentStatus Status { get; set; }
        public List<string> PackagesIds { get; set; }

        public Enrollment()
        {
            Affils = new List<string>();
            PackagesIds = new List<string>();
        }
    }
}