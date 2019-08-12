using AutoTrader.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class Site
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public SiteStatus Status { get; set; }
        public List<Enrollment> Enrollments { get; set; }
        public Logins Logins { get; set; }

        public Site()
        {
            Enrollments = new List<Enrollment>();
            Logins = new Logins
            {
                Total = 2,
                Upload = 2,
                Download = 2
            };
        }
    }
}