﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Exceptions
{
    public class UnknownSectionException : Exception
    {
        public UnknownSectionException(string sectionName) : base($"Unknown section: {sectionName}")
        {
        }
    }
}