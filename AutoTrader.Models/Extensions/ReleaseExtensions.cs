using AutoTrader.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Extensions
{
    public static class ReleaseExtensions
    {
        public static Task<ReleaseBase> ProcessRelease(this ReleaseBase release, Section section)
        {
            return Task.CompletedTask;
        }
    }
}