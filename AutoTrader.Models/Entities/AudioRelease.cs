using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class AudioRelease : ReleaseBase
    {
        public string Artist { get; set; }
        public string Title { get; set; }

        public AudioRelease(string name) : base(name)
        {
        }
    }
}