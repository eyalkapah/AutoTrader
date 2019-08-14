using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class Logins
    {
        public int Total { get; set; }
        public int Upload { get; set; }
        public int Download { get; set; }

        public void ReduceDownload(int count)
        {
            Download -= count;
            Total -= count;
        }

        internal void ReduceUpload(int count)
        {
            Upload -= count;
            Total -= cout;
        }
    }
}