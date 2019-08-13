using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Shared.Models
{
    public class PreDbContract
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("bot")]
        public string Bot { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }
    }
}