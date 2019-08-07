using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Shared.Models
{
    public class PackageContract
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("wordId")]
        public string WordId { get; set; }

        [JsonProperty("applicability")]
        public int Applicability { get; set; }
    }
}