﻿using AutoTrader.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class Word : WordBase
    {
        public string IgnorePattern { get; set; }
        public string Pattern { get; set; }
    }
}