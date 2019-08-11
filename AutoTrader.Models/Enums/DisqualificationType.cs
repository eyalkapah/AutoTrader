using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Enums
{
    public enum DisqualificationType
    {
        SiteStatusOff = 1,
        SectionNotExist,
        SectionStatusOff,
        SectionUploadOnlyAffiliate,
        SiteUploadOnlyAffiliate,
    }
}