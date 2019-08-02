using System;

namespace AutoTrader.Models.Entities
{
    public abstract class ReleaseBase
    {
        public string Group { get; set; }
        public string Name { get; set; }
        public DateTime PublishDateTime { get; set; }

        public ReleaseBase(string name)
        {
            Name = name;
            PublishDateTime = DateTime.Now;
        }
    }
}