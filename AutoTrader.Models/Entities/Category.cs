﻿using AutoTrader.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class Category
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public IList<Section> Sections { get; set; }
        public CategoryType Type { get; set; }

        public Category()
        {
            Sections = new List<Section>();
        }
    }
}