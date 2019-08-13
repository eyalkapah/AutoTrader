using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Shared.Models
{
    public class SiteContract
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("rank")]
        public int Rank { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("enrollments")]
        public EnrollmentContract[] Enrollments { get; set; }

        [JsonProperty("logins")]
        public LoginsContract Logins { get; set; }

        [JsonProperty("ircInfo")]
        public SiteIrcInfoContract[] IrcInfo { get; set; }
    }
}