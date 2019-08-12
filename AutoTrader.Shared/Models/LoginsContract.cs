using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Shared.Models
{
    public class LoginsContract
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("upload")]
        public int Upload { get; set; }

        [JsonProperty("download")]
        public int Download { get; set; }
    }
}