using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrader.Models.Entities
{
    public class Data
    {
        public List<Category> Categories { get; set; }
        public List<ComplexWord> ComplexWords { get; set; }

        public List<Package> Packages { get; set; }

        public List<PreDb> PreDbs { get; set; }

        public List<Section> Sections { get; set; }

        public List<Site> Sites { get; set; }

        public List<Word> Words { get; set; }
    }
}