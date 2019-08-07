using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Shared.Models
{
    public class EnrollmentContract
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("sectionId")]
        public string SectionId { get; set; }

        [JsonProperty("affils")]
        public string[] Affils { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("packages")]
        public PackageContract[] Packages { get; set; }
    }
}